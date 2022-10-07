using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DBContext;

namespace UserServiceTest.Systems.Test
{
    public class UnitTestMockingContext : DbContext, IUnitTestMockingContext
    {
        public UnitTestMockingContext(DbContextOptions<UnitTestMockingContext> options) 
            : base(options)
        {
        }
        public DbSet<UserService.DBContext.PatientMaster> PatientMasters { get; set; }
        public void MarkAsModified(PatientMaster item)
        {
            Entry(item).State = EntityState.Modified;
        }
    }
}
