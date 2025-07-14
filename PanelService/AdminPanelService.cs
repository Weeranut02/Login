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
        var exisitngAmid = await _context.AdminInfos.FirstOrDefaultAsync(x => x.Email == registrationModel.Email);
        if (exisitngAmid == null)
        {
            return new ResponseModel
            {
                Success = false,
                Message = "An error occured! Please try Again."
            };
        }

        var dbAdminInfos = new AdminInfos
        {
            Name = registrationModel.Name,
            Email = registrationModel.Email,
            Password = registrationModel.Password
        };

        await _context.AdminInfos.AddAsync(dbAdminInfos);
        await _context.SaveChangesAsync();

        return new ResponseModel
        {
            Success = true,
            Message = "Registretion successful."
        };
    
    }

    public async Task<ResponseModel> Login(LoginModel loginModel)
    {
        var user = await _context.AdminInfos.FirstOrDefaultAsync(x => x.Email == loginModel.EmailId);
        if (user == null)
        {
            return new ResponseModel
            {
                Success = false,
                Message = "Email not registered"
            };

        }

        if (user.Password != loginModel.Password)
        {
            return new ResponseModel
            {
                Success = false,
                Message = "Your Password is incorect"
            };

        }
        return new ResponseModel
        {
            Success = true,
            Message = $"{user.Id} {user.Name} {user.Email}"

        };
        

        
    }
}

