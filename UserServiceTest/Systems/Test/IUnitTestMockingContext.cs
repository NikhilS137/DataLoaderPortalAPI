using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DBContext;

namespace UserServiceTest.Systems.Test
{
    public interface IUnitTestMockingContext : IDisposable
    {
        DbSet<PatientMaster> PatientMasters { get; }
        int SaveChanges();
        void MarkAsModified(PatientMaster item);
    }
}
