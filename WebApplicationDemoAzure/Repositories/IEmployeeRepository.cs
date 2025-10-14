using WebApplicationDemoAzure.Models;

namespace WebApplicationDemoAzure.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task<Employee> AddAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
        Task<bool> GetUserMasterDetailsAsync(string email, string password);
    }
}
