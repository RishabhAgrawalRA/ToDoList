using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Models;

namespace ToDoListAPI.Data
{
    public class ToDoListDbContext : DbContext
    {
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<ToDoList> toDoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var toDoLists = new List<ToDoList>()
                   {
                       new(){ id = 1, title = "Buy groceries", description = "", isCompleted = false, createdAt = DateTime.Today },
                       new(){ id = 2, title = "Walk the dog", description = "", isCompleted = true, createdAt = DateTime.Today.AddDays(3) },
                       new(){ id = 3, title = "Read a book", description = "", isCompleted = false, createdAt = DateTime.Today.AddDays(-3) }
                   };

            modelBuilder.Entity<ToDoList>().HasData(toDoLists);
        }
    }
}
