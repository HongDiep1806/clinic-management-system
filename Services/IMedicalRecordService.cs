using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services
{
    public interface IMedicalRecordService
    {
        Task<MedicalRecord> CreateMedicalRecord (MedicalRecord medicalRecord);
        Task<MedicalRecord> GetMedicalRecordById(int recordId);
        Task<MedicalRecord> GetMedicalRecordByIdIncludePres(int recordId);  
        Task<List<MedicalRecord>> GetMedicalRecordsByPatientId(int patientId);
    }
}
