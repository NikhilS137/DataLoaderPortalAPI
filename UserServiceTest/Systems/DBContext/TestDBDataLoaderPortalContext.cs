using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DBContext;

namespace UserServiceTest.Systems.DBContext
{
    public class TestDBDataLoaderPortalContext : DBDataLoaderPortalContext
    {
            public TestDBDataLoaderPortalContext()
            {
                this.PatientMasters = new TestPatientMasterDbSet();
            }

            public DbSet<PatientMaster> PatientMasters { get; set; }

            public int SaveChanges()
            {
                return 0;
            }

            public void MarkAsModified(PatientMaster item) { }
            public void Dispose() { }
    }
}

