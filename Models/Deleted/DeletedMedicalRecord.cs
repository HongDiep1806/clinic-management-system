using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models.Deleted
{
    public class DeletedMedicalRecord
    {
        [Key]
        public int DeletedRecordId { get; set; }
        public int RecordId { get; set; }
        public int AppointmentId { get; set; }
        public string DiagnosisDescription { get; set; }
        public string Treatment { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
