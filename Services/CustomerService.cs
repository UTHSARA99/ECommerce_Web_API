using e_commerce_web_api.Models;
using e_commerce_web_api.Repositories;

namespace e_commerce_web_api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            return await _customerRepository.CreateAsync(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
            {
                throw new ArgumentException($"Customer with ID {id} not found.");
            }

            await _customerRepository.DeleteAsync(customer);
        }

        public Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return _customerRepository.GetAllAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task UpdateCustomerAsync(int id, Customer customer)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(id);

            if (existingCustomer != null)
            {
                var emailExisting = await _customerRepository.CheckEmailAsync(customer.Email, id);
                if (emailExisting)
                {
                    throw new InvalidOperationException($"Email {customer.Email} is already in use by another customer.");
                }
                existingCustomer.Name = customer.Name;
                existingCustomer.Email = customer.Email;

                await _customerRepository.UpdateAsync(existingCustomer);
            }
            else
            {
                throw new ArgumentException($"Customer with ID {id} not found.");
            }
        }
    }
}
