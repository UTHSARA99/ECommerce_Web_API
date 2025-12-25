using e_commerce_web_api.Models;
using e_commerce_web_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // GET: api/order/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        // POST: api/order
        [HttpPost]
        public async Task<ActionResult<Order>> Create(Order order)
        {
            if (order == null)
            {
                return BadRequest("Order cannot be null");
            }

            var createdOrder = await _orderService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = createdOrder.Id }, createdOrder);
        }

        // DELETE: api/order/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _orderService.GetOrderById(id);
            if (existing == null)
                return NotFound();

            await _orderService.DeleteOrderAsync(id);
            return NoContent();

        }

        // PUT: api/order
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Order order)
        {
            var updatedOrder = await _orderService.UpdateOrderAsync(order);
            if (updatedOrder == null)
            {
                return NotFound();
            }
            return Ok(updatedOrder);
        }
    }
}
