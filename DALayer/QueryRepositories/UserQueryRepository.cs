﻿using DALayer.DataBase;
using DomainLayer.Abstarction.IQueryRepositories;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DALayer.QueryRepositories;

public class UserQueryRepository : IUserQueryRepository
{
    private readonly AppDbContext _context;

    public UserQueryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public Task<User> GetUserById(int userId)
    {
        throw new NotImplementedException();
    }
}