using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsersLogin.Models;
using UsersLogin.Services;

namespace UsersLogin.PanelService;

public class AdminPanelService
{
    private readonly ApplicationDbContext _context;

    public AdminPanelService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel> Register(RegistrationModel registrationModel)
    {
        // Check if email already exists
        var existingAdmin = await _context.AdminInfos.FirstOrDefaultAsync(x => x.Email == registrationModel.Email);
        if (existingAdmin != null)
        {
            return new ResponseModel
            {
                Success = false,
                Message = "Email already registered. Please use a different email."
            };
        }

        var dbAdminInfos = new AdminInfos
        {
            Name = registrationModel.Name,
            Email = registrationModel.Email,
            Password = registrationModel.Password // Note: In production, you should hash the password
        };

        await _context.AdminInfos.AddAsync(dbAdminInfos);
        await _context.SaveChangesAsync();

        return new ResponseModel
        {
            Success = true,
            Message = "Registration successful."
        };
    }

    public async Task<ResponseModel> Login(LoginModel loginModel)
    {
        // Validate input
        if (loginModel == null || string.IsNullOrEmpty(loginModel.EmailId) || string.IsNullOrEmpty(loginModel.Password))
        {
            return new ResponseModel
            {
                Success = false,
                Message = "Email and password are required"
            };
        }

        // Find user by email
        var user = await _context.AdminInfos.FirstOrDefaultAsync(x => x.Email == loginModel.EmailId);
        if (user == null)
        {
            // Don't reveal whether email exists (security best practice)
            return new ResponseModel
            {
                Success = false,
                Message = "Invalid credentials"
            };
        }

        // Verify password - in production, you should NEVER store or compare plain text passwords
        // This is just for demonstration - see important security note below
        if (user.Password != loginModel.Password)
        {
            return new ResponseModel
            {
                Success = false,
                Message = "Invalid credentials"
            };
        }

        // Successful login
        return new ResponseModel
        {
            Success = true,
            Message = $"{user.Id} {user.Name} {user.Email}"
        };
    }
}

