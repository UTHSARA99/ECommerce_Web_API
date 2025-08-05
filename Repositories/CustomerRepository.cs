using e_commerce_web_api.Models;

namespace e_commerce_web_api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public Task<Customer> CreateCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCustomerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetCustomerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
