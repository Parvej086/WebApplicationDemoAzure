using Microsoft.EntityFrameworkCore;
using WebApplicationDemoAzure.Data;
using WebApplicationDemoAzure.Models;

namespace WebApplicationDemoAzure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EmployeeDbContext _context;
        public ProductRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.products.FindAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            await _context.products.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            _context.products.Update(product);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
                _context.products.Remove(product);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
