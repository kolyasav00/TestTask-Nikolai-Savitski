using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTask.Models;
using TestTask.Data;

namespace TestTask.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<Order?> GetOrder();
        public Task<List<Order>> GetOrders();
    }
    
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetOrder()
        {
            var orderWithMaxAmount = await _context.Orders
                .OrderByDescending(o => o.Price * o.Quantity)
                .FirstOrDefaultAsync();

            return orderWithMaxAmount;
        }

        public async Task<List<Order>> GetOrders()
        {
            var ordersWithQuantityGreaterThan10 = await _context.Orders
                .Where(o => o.Quantity > 10)
                .ToListAsync();

            return ordersWithQuantityGreaterThan10;
        }
    }
}
