using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DBContext;

namespace UserServiceTest.MockData
{
    public class PatientMasterMockData
    {
        public static List<PatientMaster> GetPatientMasters()
        {
            return new List<PatientMaster> {
                new PatientMaster {
                    Id = 1,
                    Address1="Add1",
                    Address2 ="Add2",
                Address3="Add 3",
                District ="Sangli",
                State ="Maharashtra",
                Country = "India",
                Dob = new DateTime(1990,12,12),
                EmailId ="Admin@gmail.com",
                PhoneNumber ="12345678890",
                DrugId = "12344",
                DrugName = "Drug 1"
                },
                 new PatientMaster {
                    Id = 2,
                    Address1="Add2",
                    Address2 ="Add2",
                Address3="Add 2",
                District ="Pune",
                State ="Maharashtra",
                Country = "India",
                Dob = new DateTime(1990,12,12),
                EmailId ="Pune@gmail.com",
                PhoneNumber ="12345678890",
                DrugId = "12342",
                DrugName = "Drug 2"
                }
       };
    }
    }
}
