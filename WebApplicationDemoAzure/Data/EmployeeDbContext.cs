using Microsoft.EntityFrameworkCore;
using WebApplicationDemoAzure.Models;

namespace WebApplicationDemoAzure.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
