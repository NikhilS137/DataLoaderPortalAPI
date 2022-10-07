using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DBContext;
using UserServiceTest.MockData;

namespace UserServiceTest.Systems.DBContext
{
    public class TestPatientMasterDbSet : TestDbSet<PatientMaster>
    {
        public override PatientMaster Find(params object[] keyValues)
        {
            return this.SingleOrDefault(item => item.Id == (int)keyValues.Single());
        }
    }
}
