using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<User> GetUser() // Возвращать пользователя с самым большим количеством заказов
    {
        //User user = await _context.Orders.GroupBy();
        var userId = _context.Orders.GroupBy(o => o.UserId)
            .OrderByDescending(bd => bd.Count())
            .Take(1)
            .Select(id => id.Key).Single();

        Task<User> user = _context.Users.Include(u => u.Orders).FirstOrDefaultAsync(u => u.Id == userId)!;

        return user;
    }

    public Task<List<User>> GetUsers() // Возвращать пользователей с статусом Inaсtive
    {
        return _context.Users.Where(u => u.Status == UserStatus.Inactive).Include(u => u.Orders).ToListAsync();
    }
}