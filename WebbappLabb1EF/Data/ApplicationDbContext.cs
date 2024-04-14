using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebbappLabb1EF.Models;

namespace WebbappLabb1EF.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       public DbSet<Employee> Employees { get; set; }
       public DbSet<Leave> Leaves { get; set; }
       public DbSet<LeaveRequest> LeaveRequests { get; set; } 
    }
}
