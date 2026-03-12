using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services
{
    public interface IMedicalRecordService
    {
        Task<MedicalRecord> CreateMedicalRecord (MedicalRecord medicalRecord);
        Task<MedicalRecord> GetMedicalRecordById(int recordId);
        Task<MedicalRecord> GetMedicalRecordByIdIncludePres(int recordId);  
        Task<List<MedicalRecord>> GetMedicalRecordsByPatientId(int patientId);
        Task<MedicalRecord> GetMedicalRecordByAppointmentId(int appointmentId);
        Task<bool> UpdateMedicalRecord(int id, MedicalRecord medicalRecord);
        Task<List<MedicalRecord>> GetMedicalRecordsByDoctorId(int doctorId);
    }
}
