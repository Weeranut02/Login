using Microsoft.EntityFrameworkCore;
using UsersLogin.Models;

namespace UsersLogin.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AdminInfos> AdminInfos { get; set; }
    }
}
