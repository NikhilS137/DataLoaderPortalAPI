using System;
using System.Collections.Generic;

namespace UserService.DBContext
{
    public partial class ValidationFailedMaster
    {
        public int Id { get; set; }
        public string? PatientName { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string? District { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Dob { get; set; }
        public string? EmailId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? DrugId { get; set; }
        public string? DrugName { get; set; }
        public string? Status { get; set; }
        public string? ErrorMessage { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? FileId { get; set; }
    }
}
