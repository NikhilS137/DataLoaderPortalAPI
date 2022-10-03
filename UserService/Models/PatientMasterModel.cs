namespace UserService.Models
{
    public class PatientMasterModel
    {
        public int Id { get; set; }
        public string Address1 { get; set; } = null!;
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string District { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string EmailId { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
