using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IMedicalRecordRepository:IBaseRepository<MedicalRecord>
    {
        Task<MedicalRecord> GetMedicalRecordByIdIncludePres (int recordId);
        Task<List<MedicalRecord>> GetMedicalRecordsByPatientId(int patientId);
        Task<MedicalRecord> GetMedicalRecordByAppointmentId(int appointmentId);
        Task<List<MedicalRecord>> GetMedicalRecordsByDoctorId(int doctorId);

    }
}
