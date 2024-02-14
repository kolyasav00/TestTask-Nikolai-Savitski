using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Enums;
using TestTask.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;

namespace TestTask.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User?> GetUser();

        public Task<List<User>> GetUsers();
    }
    
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUser()
        {
            var userWithMostOrders = await _context.Users.
                OrderByDescending(u => u.Orders.Count).FirstOrDefaultAsync();
            
            return userWithMostOrders;
        }

        public async Task<List<User>> GetUsers()
        {
            var inactiveUsers = await _context.Users.
                Where(u => u.Status == UserStatus.Inactive).ToListAsync();
            
            return inactiveUsers;
        }
    }
}
