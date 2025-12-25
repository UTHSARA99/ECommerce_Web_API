using e_commerce_web_api.Data;
using e_commerce_web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_web_api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbcontext;

        public OrderRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            using var transaction = await _dbcontext.Database.BeginTransactionAsync();
            
            try
            {
                // Store order items and clear the navigation property
                var orderItems = order.OrderItems?.ToList() ?? new List<OrderItem>();
                order.OrderItems = null;
                
                // Add and save the order first
                _dbcontext.Orders.Add(order);
                await _dbcontext.SaveChangesAsync(); // It is at this point the order.Id attribute is populated by EF. (For my reference)
                
                // Now create new OrderItem objects with the correct OrderId
                if (orderItems.Any())
                {
                    var newOrderItems = orderItems.Select(item => new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    }).ToList();
                    
                    _dbcontext.OrderItems.AddRange(newOrderItems);
                    await _dbcontext.SaveChangesAsync();
                    
                    // Set the navigation property
                    order.OrderItems = newOrderItems;
                }
                
                await transaction.CommitAsync();
                return order;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _dbcontext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _dbcontext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task DeleteAsync(Order order)
        {
            _dbcontext.Orders.Remove(order);
            await _dbcontext.SaveChangesAsync();
        }

        public Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Order order)
        {
            _dbcontext.Orders.Update(order);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
