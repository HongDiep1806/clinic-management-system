using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;

namespace ClinicManagementSystem.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _medicalRecordRpository;

        public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRpository = medicalRecordRepository;
        }
        public Task<MedicalRecord> CreateMedicalRecord(MedicalRecord medicalRecord)
        {
            return _medicalRecordRpository.Create(medicalRecord);

        }

        public async Task<MedicalRecord> GetMedicalRecordById(int recordId)
        {
            return await _medicalRecordRpository.GetById(recordId);
        }

        public async Task<MedicalRecord> GetMedicalRecordByIdIncludePres(int recordId)
        {
            return await _medicalRecordRpository.GetMedicalRecordByIdIncludePres(recordId);
        }

        public async Task<List<MedicalRecord>> GetMedicalRecordsByPatientId(int patientId)
        {
            return await _medicalRecordRpository.GetMedicalRecordsByPatientId(patientId);
        }
    }

}
