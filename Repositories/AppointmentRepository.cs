using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
        }
        
    }
   
}
