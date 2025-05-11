namespace ClinicManagementSystem.Services
{
    public interface IUserRoleService
    {
        Task<List<string>> GetUserRoles(int userId);
    }
}
