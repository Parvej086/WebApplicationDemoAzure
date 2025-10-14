using Microsoft.EntityFrameworkCore;
using WebApplicationDemoAzure.Data;
using WebApplicationDemoAzure.Models;

namespace WebApplicationDemoAzure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> GetUserMasterDetailsAsync(string email, string password)
        {
            try
            {
                var data = await _context.UserMaster.Where(x=>x.Email_Id==email && x.Password==password).FirstOrDefaultAsync().ConfigureAwait(false);
                if (data != null) return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
