using Azure.Core;
using ClinicManagementSystem.DAL;
using ClinicManagementSystem.DTOs.User;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Models.Deleted;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ClinicManagementSystem.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserRepository(ApplicationDbContext context, RestoreDbContext restoreContext, IPasswordHasher<User> passwordHasher)
    : base(context, restoreContext)
        {
            _passwordHasher = passwordHasher;
        }



        public Task<bool> CheckPasswordAsync(User user, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, password);
            return Task.FromResult(result == PasswordVerificationResult.Success);
        }

        public Task<bool> DoesDoctorExits(int doctorId)
        {
            var doctor = _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefault(u => u.UserId == doctorId && u.UserRoles.Any(ur => ur.Role.RoleName == "Doctor"));
            if (doctor == null)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool> DoesPatientExists(int patientId)
        {
            var patient = _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefault(u => u.UserId == patientId && u.UserRoles.Any(ur => ur.Role.RoleName == "Patient"));
            if (patient == null)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {

            return await _context.Users
                //.Include(u => u.Department)
                //.Include(u => u.AppointmentsAsPatient)
                //.Include(u => u.AppointmentsAsDoctor)
                //.Include(u => u.Schedules)
                .Include(u => u.UserRoles)
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<User>> GetDoctorUsers()
        {
            return await _context.Set<User>()
               .Include(u => u.UserRoles)
                   .ThenInclude(ur => ur.Role)
               .Where(u => u.UserRoles.Any(ur => ur.Role.RoleName == "Doctor"))
               .ToListAsync();
        }

        public async Task<List<User>> GetPatientUsers()
        {
            return await _context.Set<User>()
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Where(u => u.UserRoles.Any(ur => ur.Role.RoleName == "Patient"))
                .ToListAsync();
        }

        public async Task<List<User>> GetUsersByRole(string roleName)
        {
            var users = await _context.Set<User>()
                                       .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                                       .Include(u => u.Department)
                                       .Where(u => u.UserRoles.Any(ur => ur.Role.RoleName.ToLower() == roleName.ToLower()))
                                       .ToListAsync();
            return users;
        }

        public async Task<User> EditUser(int userId, EditUserDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return null;

            // 🔥 Check trùng email
            if (!string.IsNullOrEmpty(dto.Email) && dto.Email != user.Email)
            {
                bool emailExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
                if (emailExists)
                    throw new BadHttpRequestException("EMAIL_IN_USE", 409);

                user.Email = dto.Email;  // Cập nhật email
            }

            user.FullName = dto.FullName;
            user.Dob = dto.Dob;
            user.Gender = dto.Gender;
            user.Phone = dto.Phone;
            user.Address = dto.Address;
            user.DepartmentId = dto.DepartmentId;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<bool> DeleteUser(int userId)
        {
            // 1) Lấy user từ DB chính
            var user = await _context.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                return false;

            // 2) LẤY ROLEID TỪ BẢNG USERROLES
            var userRoleId = await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .FirstOrDefaultAsync();

            // 3) LẤY ROLENAME TỪ BẢNG ROLES
            var roleName = await _context.Roles
                .Where(r => r.RoleId == userRoleId)
                .Select(r => r.RoleName)
                .FirstOrDefaultAsync() ?? "Unknown";

            // 4) Tạo record DeletedUser
            var deleted = new DeletedUser
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Dob = user.Dob,
                Gender = user.Gender,
                Phone = user.Phone,
                Address = user.Address,
                Email = user.Email,
                HashPassword = user.HashPassword,
                DepartmentId = user.DepartmentId ?? 0,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                RoleName = roleName
            };

            // 5) Lưu vào RestoreDB
            await _restoreContext.DeletedUsers.AddAsync(deleted);
            await _restoreContext.SaveChangesAsync();

            // 6) Xóa user khỏi DB chính
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<List<dynamic>> GetAllPatientsWithStatus()
        {
            var patients = await _context.Users
                .Include(u => u.UserRoles)
                .Where(u => u.UserRoles.Any(ur => ur.Role.RoleName == "Patient"))
                .ToListAsync();

            var inactiveIds = await _restoreContext.DeletedUsers
                .Where(d => d.RoleName == "Patient")
                .Select(d => d.UserId)
                .ToListAsync();

            return patients.Select(p => new
            {
                p.UserId,
                p.FullName,
                p.Email,
                p.Phone,
                p.Gender,
                p.Address,
                Status = inactiveIds.Contains(p.UserId) ? "Inactive" : "Active"
            }).ToList<dynamic>();
        }
        public async Task<List<dynamic>> GetAllDoctorsWithStatus()
        {
            // 1️⃣ Lấy tất cả doctor từ Active DB
            var active = await _context.Users
                .Include(u => u.UserRoles)
                .Where(u => u.UserRoles.Any(ur => ur.Role.RoleName == "Doctor"))
                .Select(u => new
                {
                    u.UserId,
                    u.FullName,
                    u.Email,
                    u.Phone,
                    u.Gender,
                    u.Address,
                    u.DepartmentId,
                    Status = "Active" // mặc định là Active
                })
                .ToListAsync();

            // 2️⃣ Lấy tất cả doctor đã deactivate (DeletedUsers)
            var deletedIds = await _restoreContext.DeletedUsers
                .Where(d => d.RoleName == "Doctor")
                .Select(d => d.UserId)
                .ToListAsync();

            // 3️⃣ Duyệt Active, nếu UserId nằm trong DeletedUsers → set Status = Inactive
            var finalList = active.Select(u => new
            {
                u.UserId,
                u.FullName,
                u.Email,
                u.Phone,
                u.Gender,
                u.Address,
                u.DepartmentId,
                Status = deletedIds.Contains(u.UserId) ? "Inactive" : "Active"
            }).ToList<dynamic>();

            return finalList;
        }
        private async Task<string?> GetRoleBeforeDeleted(int userId)
        {
            var role = await _context.UserRoles
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role.RoleName)
                .FirstOrDefaultAsync();

            return role;
        }

        public async Task<List<UserStatusDto>> GetUsersByRoleWithStatus(string roleName)
        {
            var result = new List<UserStatusDto>();

            // ACTIVE USERS
            var activeUsers = await _context.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .Where(u => u.UserRoles.Any(ur => ur.Role.RoleName == roleName))
                .Select(u => new UserStatusDto
                {
                    UserId = u.UserId,
                    FullName = u.FullName,
                    Email = u.Email,
                    Phone = u.Phone,
                    Gender = u.Gender,
                    Address = u.Address,
                    RoleName = roleName,
                    Status = "Active",
                    Dob = u.Dob,                    // ✅ THÊM DOB VÔ
                    DepartmentId = u.DepartmentId,

                })
                .ToListAsync();

            // DELETED USERS
            var deletedUsers = await _restoreContext.DeletedUsers
                .Where(d => d.RoleName == roleName)  // dùng RoleName trực tiếp
                .Select(d => new UserStatusDto
                {
                    UserId = d.UserId,
                    FullName = d.FullName,
                    Email = d.Email,
                    Phone = d.Phone,
                    Gender = d.Gender,
                    Address = d.Address,
                    RoleName = d.RoleName,
                    Status = "Inactive",
                    Dob = d.Dob,                    // ✅ THÊM DOB VÔ
                    DepartmentId = d.DepartmentId,

                })
                .ToListAsync();

            return activeUsers.Concat(deletedUsers).ToList();
        }
        public async Task<bool> ToggleUserStatus(int userId)
        {
            // 1️⃣ Check xem user đang inactive không (tức là nằm trong DeletedUsers)
            var deletedRecord = await _restoreContext.DeletedUsers
                .FirstOrDefaultAsync(d => d.UserId == userId);

            if (deletedRecord != null)
            {
                // 👉 Restore = chỉ cần xóa record trong DeletedUsers
                _restoreContext.DeletedUsers.Remove(deletedRecord);
                await _restoreContext.SaveChangesAsync();
                return true; // ACTIVE
            }

            // 2️⃣ Nếu chưa bị inactive → user nằm trong Users
            var activeUser = await _context.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (activeUser == null)
                return false;

            // Lấy role
            var roleName = activeUser.UserRoles.First().Role.RoleName;

            // 3️⃣ Kiểm tra appointments để block deactivate
            if (roleName == "Doctor")
            {
                bool hasConfirmed = await _context.Appointments
                    .AnyAsync(a => a.DoctorId == userId && a.Status == AppointmentStatus.Confirmed);

                if (hasConfirmed)
                    throw new BadHttpRequestException("DOCTOR_HAS_CONFIRMED_APPOINTMENTS", 409);

                // Cancel pending
                var pending = _context.Appointments
                    .Where(a => a.DoctorId == userId && a.Status == AppointmentStatus.Pending);

                await pending.ForEachAsync(a => a.Status = AppointmentStatus.Cancelled);
            }
            else if (roleName == "Patient")
            {
                bool hasConfirmed = await _context.Appointments
                    .AnyAsync(a => a.PatientId == userId && a.Status == AppointmentStatus.Confirmed);

                if (hasConfirmed)
                    throw new BadHttpRequestException("PATIENT_HAS_CONFIRMED_APPOINTMENTS", 409);

                var pending = _context.Appointments
                    .Where(a => a.PatientId == userId && a.Status == AppointmentStatus.Pending);

                await pending.ForEachAsync(a => a.Status = AppointmentStatus.Cancelled);

            }
            await _context.SaveChangesAsync(); 


            // 4️⃣ Thêm record inactive vào DeletedUsers
            var del = new DeletedUser
            {
                UserId = activeUser.UserId,
                FullName = activeUser.FullName,
                Dob = activeUser.Dob,
                Gender = activeUser.Gender,
                Phone = activeUser.Phone,
                Address = activeUser.Address,
                Email = activeUser.Email,
                HashPassword = activeUser.HashPassword,
                DepartmentId = activeUser.DepartmentId ?? 0,
                CreatedAt = activeUser.CreatedAt,
                UpdatedAt = activeUser.UpdatedAt,
                RoleName = roleName
            };

            await _restoreContext.DeletedUsers.AddAsync(del);
            await _restoreContext.SaveChangesAsync();

            return true; // INACTIVE
        }


        // ==============================================
        // SUPPORT METHODS
        // ==============================================

        private async Task<bool> SoftDelete(User activeUser, string roleName)
        {
            var deleted = new DeletedUser
            {
                UserId = activeUser.UserId,
                FullName = activeUser.FullName,
                Dob = activeUser.Dob,
                Gender = activeUser.Gender,
                Phone = activeUser.Phone,
                Address = activeUser.Address,
                Email = activeUser.Email,
                HashPassword = activeUser.HashPassword,
                DepartmentId = activeUser.DepartmentId ?? 0,
                CreatedAt = activeUser.CreatedAt,
                UpdatedAt = activeUser.UpdatedAt,
                RoleName = roleName
            };

            await _restoreContext.DeletedUsers.AddAsync(deleted);
            await _restoreContext.SaveChangesAsync();

            _context.Users.Remove(activeUser);
            await _context.SaveChangesAsync();

            return true;
        }


        // ================= SUPPORT METHODS =================

        private async Task<bool> SoftDeleteUser(User activeUser, string roleName)
        {
            var deleted = new DeletedUser
            {
                UserId = activeUser.UserId,
                FullName = activeUser.FullName,
                Dob = activeUser.Dob,
                Gender = activeUser.Gender,
                Phone = activeUser.Phone,
                Address = activeUser.Address,
                Email = activeUser.Email,
                HashPassword = activeUser.HashPassword,
                DepartmentId = activeUser.DepartmentId ?? 0,
                CreatedAt = activeUser.CreatedAt,
                UpdatedAt = activeUser.UpdatedAt,
                RoleName = roleName
            };

            await _restoreContext.DeletedUsers.AddAsync(deleted);
            await _restoreContext.SaveChangesAsync();

            _context.Users.Remove(activeUser);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> RestoreUser(DeletedUser deletedU, string newEmail)
        {
            int? dep = deletedU.DepartmentId == 0 ? null : deletedU.DepartmentId;

            string sql = @"
        SET IDENTITY_INSERT Users ON;

        INSERT INTO Users 
        ([UserId], [FullName], [Dob], [Gender], [Phone], [Address],
         [Email], [HashPassword], [DepartmentId], [CreatedAt], [UpdatedAt])
        VALUES (@UserId, @FullName, @Dob, @Gender, @Phone, @Address,
                @Email, @HashPassword, @DepartmentId, @CreatedAt, @UpdatedAt);

        SET IDENTITY_INSERT Users OFF;
    ";

            await _context.Database.ExecuteSqlRawAsync(sql,
                new[]
                {
            new SqlParameter("@UserId", deletedU.UserId),
            new SqlParameter("@FullName", deletedU.FullName ?? (object)DBNull.Value),
            new SqlParameter("@Dob", deletedU.Dob),
            new SqlParameter("@Gender", deletedU.Gender ?? (object)DBNull.Value),
            new SqlParameter("@Phone", deletedU.Phone ?? (object)DBNull.Value),
            new SqlParameter("@Address", deletedU.Address ?? (object)DBNull.Value),
            new SqlParameter("@Email", newEmail),
            new SqlParameter("@HashPassword", deletedU.HashPassword ?? (object)DBNull.Value),
            new SqlParameter("@DepartmentId", dep ?? (object)DBNull.Value),
            new SqlParameter("@CreatedAt", deletedU.CreatedAt),
            new SqlParameter("@UpdatedAt", DateTime.UtcNow)
                }
            );

            // Restore role
            var restoredRoleId = await _context.Roles
                .Where(r => r.RoleName == deletedU.RoleName)
                .Select(r => r.RoleId)
                .FirstOrDefaultAsync();

            await _context.UserRoles.AddAsync(new UserRole
            {
                UserId = deletedU.UserId,
                RoleId = restoredRoleId
            });

            await _context.SaveChangesAsync();

            _restoreContext.DeletedUsers.Remove(deletedU);
            await _restoreContext.SaveChangesAsync();

            return true;
        }
        public async Task<DeletedUser?> GetDeletedUserById(int userId)
        {
            return await _restoreContext.DeletedUsers
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
        public Task<bool> EmailExists(string email)
        {
            return _context.Users.AnyAsync(u => u.Email == email);
        }
        public async Task<dynamic?> GetUserByIdWithStatus(int userId)
        {
            // 1) Tìm trong Users (Active)
            var user = await _context.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user != null)
            {
                return new
                {
                    user.UserId,
                    user.FullName,
                    user.Email,
                    user.Phone,
                    user.Gender,
                    user.Address,
                    user.Dob,
                    user.DepartmentId,
                    DepartmentName = user.Department != null ? user.Department.Name : null,
                    Status = "Active",
                    user.CreatedAt,
                    user.UpdatedAt
                };
            }

            // 2) Nếu không có → tìm trong DeletedUsers (Inactive)
            var deleted = await _restoreContext.DeletedUsers
                .FirstOrDefaultAsync(d => d.UserId == userId);

            if (deleted != null)
            {
                return new
                {
                    deleted.UserId,
                    deleted.FullName,
                    deleted.Email,
                    deleted.Phone,
                    deleted.Gender,
                    deleted.Address,
                    deleted.Dob,
                    deleted.DepartmentId,
                    // restore DB không có navigation -> tự map
                    DepartmentName = await _context.Departments
                        .Where(d => d.DepartmentId == deleted.DepartmentId)
                        .Select(d => d.Name)
                        .FirstOrDefaultAsync(),
                    Status = "Inactive",
                    deleted.CreatedAt,
                    deleted.UpdatedAt
                };
            }

            // 3) Không tìm thấy user
            return null;
        }








    }
}
