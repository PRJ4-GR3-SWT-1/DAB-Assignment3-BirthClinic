namespace DAB_Assignment3_BirthClinic.Models
{
    public class ClinicianBirth
    {
        public int ClinicianBirthId { get; set; }
        public Birth Birth { get; set; }
        public Clinician Clinician { get; set; }
    }
}