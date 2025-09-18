using e_commerce_web_api.Data;
using e_commerce_web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_web_api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _dbcontext;

        public CustomerRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            _dbcontext.Customers.Add(customer);
            await _dbcontext.SaveChangesAsync();
            return customer;
        }

        public async Task DeleteAsync(Customer customer)
        {
            _dbcontext.Customers.Remove(customer);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _dbcontext.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _dbcontext.Customers.FindAsync(id);
        }

        public async Task UpdateAsync(Customer customer)
        {
            _dbcontext.Customers.Update(customer);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<bool> CheckEmailAsync(string email, int? id)
        {
            return await _dbcontext.Customers
                .Where(c => c.Email == email && (id == null || c.Id != id))
                .AnyAsync();
        }
    }
}
