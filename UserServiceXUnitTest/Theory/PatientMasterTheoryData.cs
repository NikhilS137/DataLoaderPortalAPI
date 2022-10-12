using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Dtos;
using UserService.Models;

namespace UserServiceXUnitTest.Theory
{
    public class PatientMasterTheoryData : TheoryData<PatientMasterDtos>
    {
        public PatientMasterTheoryData()
        {
            /**
             * Each item you add to the TheoryData collection will try to pass your unit test's one by one.
             */

            Add(new PatientMasterDtos()
            {
                Id = 1,
                PatientName = "P1",
                Address1 = "Add1",
                Address2 = "Add1",
                Address3 = "Add1",
                District = "Sangli",
                State = "Maharashtra",
                Country = "India",
                Dob = DateTime.Now,
                EmailId = "P1@gmail.com",
                PhoneNumber = "1234556670",
                DrugId = "12345-1234-11",
                DrugName = "Drug 1",
                Status = "Pending"
            });
        }
    }
}
