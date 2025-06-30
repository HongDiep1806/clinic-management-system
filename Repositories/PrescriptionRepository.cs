using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public class PrescriptionRepository : BaseRepository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
