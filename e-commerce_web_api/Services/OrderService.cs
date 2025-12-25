using e_commerce_web_api.Models;
using e_commerce_web_api.Repositories;

namespace e_commerce_web_api.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderRepository _orderRepository;
        
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            if (order.OrderDate == default)
                order.OrderDate = DateTime.UtcNow;

            return await _orderRepository.CreateAsync(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order != null)
            {
                await _orderRepository.DeleteAsync(order);
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            //have to get the existing order
            var existingOrder = await _orderRepository.GetByIdAsync(order.Id);

            if (existingOrder == null)
            {
                throw new ArgumentException($"Order with ID {order.Id} not found.");
            }

            // Change the order items. No reason to let change the order date or customer ID. If needed can add last updated time.
            if (order.OrderItems != null)
            {
                // Remove existing order items that are not in the new list
                var existingItemIds = existingOrder.OrderItems.Select(oi => oi.ProductId).ToHashSet();
                var newItemIds = order.OrderItems.Select(oi => oi.ProductId).ToHashSet();

                var itemsToRemove = existingOrder.OrderItems
                    .Where(oi => !newItemIds.Contains(oi.ProductId))
                    .ToList();

                foreach (var item in itemsToRemove)
                {
                    existingOrder.OrderItems.Remove(item);
                }

                // Update or add order items
                foreach (var newItem in order.OrderItems)
                {
                    var existingItem = existingOrder.OrderItems
                        .FirstOrDefault(oi => oi.ProductId == newItem.ProductId);

                    if (existingItem != null)
                    {
                        // Update existing item
                        existingItem.Quantity = newItem.Quantity;
                    }
                    else
                    {
                        // Add new item
                        existingOrder.OrderItems.Add(new OrderItem
                        {
                            OrderId = order.Id,
                            ProductId = newItem.ProductId,
                            Quantity = newItem.Quantity
                        });
                    }
                }
            }
            await _orderRepository.UpdateAsync(existingOrder);
            return existingOrder;

        }
    }
}