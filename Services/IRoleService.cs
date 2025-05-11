namespace ClinicManagementSystem.Services
{
    public interface IRoleService
    {
        Task<string> GetRoleNameById(int roleId);
    }
}
