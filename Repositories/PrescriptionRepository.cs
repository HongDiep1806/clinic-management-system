using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Repositories
{
    public class PrescriptionRepository : BaseRepository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<List<Prescription>> GetByRecordId(int recordId)
        {
            return await _context.Prescriptions
                .Include(p => p.Medicine)
                .Where(p => p.RecordId == recordId)
                .ToListAsync();
        }
        public async Task<bool> DeletePrescription(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);

            if (prescription == null)
                return false;

            _context.Prescriptions.Remove(prescription);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
