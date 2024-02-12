﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Enums;
using TestTask.Models;
using System.Linq;
using TestTask.Data;

namespace TestTask.Services.Interfaces
{
    
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser()
        {
            var userWithMostOrders = _context.Users.OrderByDescending(u => u.Orders.Count).FirstOrDefault();
            return userWithMostOrders;
        }

        public async Task<List<User>> GetUsers()
        {
            var inactiveUsers = _context.Users.Where(u => u.Status == UserStatus.Inactive).ToList();
            return inactiveUsers;
        }
    }
    public interface IUserService
    {
        public Task<User?> GetUser();

        public Task<List<User>> GetUsers();
    }
}