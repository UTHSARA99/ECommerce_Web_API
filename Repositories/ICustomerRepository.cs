using e_commerce_web_api.Models;

namespace e_commerce_web_api.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<Customer> CreateAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task<bool> CheckEmailAsync(string email, int? id);
        Task DeleteAsync(Customer customer);

    }
}
