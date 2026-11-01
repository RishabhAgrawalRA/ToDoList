using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Models.UserModel;

namespace ToDoListAPI.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<User> Users { get; set; }

    }
}
