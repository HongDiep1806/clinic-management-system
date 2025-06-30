using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services
{
    public interface IPrescriptionService
    {
        Task<Prescription> CreatePrescription(Prescription prescription);
    }
}
