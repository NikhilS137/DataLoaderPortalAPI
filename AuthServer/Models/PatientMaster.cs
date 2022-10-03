using System;
using System.Collections.Generic;

namespace AuthServer.Models
{
    public partial class PatientMaster
    {
        public int Id { get; set; }
        public string PatientName { get; set; } = null!;
        public string Address1 { get; set; } = null!;
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string District { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string EmailId { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string DrugId { get; set; } = null!;
        public string DrugName { get; set; } = null!;
        public string? Status { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
