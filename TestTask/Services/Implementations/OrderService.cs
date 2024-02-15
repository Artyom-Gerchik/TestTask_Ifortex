using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations;

public class OrderService : IOrderService
{
    
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }
    public Task<Order> GetOrder() // Возвращать заказ с самой большой суммой заказа
    {
        return _context.Orders.OrderByDescending(o => o.Price)
            .FirstOrDefaultAsync()!;
    }

    public Task<List<Order>> GetOrders() // Возвращать заказы в которых количество товара больше 10
    {
        return _context.Orders.Where(o => o.Quantity > 10).ToListAsync();
    }
}