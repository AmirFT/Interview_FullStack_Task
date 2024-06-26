﻿using BackEnd.Domain.Entities;
using BackEnd.Domain.Enums;
using BackEnd.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BackEnd.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole("Administrator");

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
        }

        // Default data
        // Seed, if necessary
        if (!_context.Employees.Any())
        {

            for (int i = 1; i <= 10; i++)
            {
                _context.Employees.Add(new Employee
                {
                    Name = $"Employee {i}",
                });
            }
            await _context.SaveChangesAsync();
        }

        if (!_context.TodoItems.Any())
        {

            var random = new Random();
            for (int i = 1; i <= 20; i++)
            {
                _context.TodoItems.Add(new TodoItem
                {
                    Id = i, // Assuming Id is generated manually; otherwise, it can be omitted if it is auto-generated
                    Title = $"Task Title {i}",
                    Description = $"Task Description {i}",
                    Status = random.Next(0, 5), // Randomly set the status between 0 and 4
                    Priority = (PriorityLevel)random.Next(0, 4), // Randomly set the priority enum value
                    EmployeeId = random.Next(1, 11) // Assign the task to a random employee
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}

