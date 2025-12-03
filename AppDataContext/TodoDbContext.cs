using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.AppDataContext
{
    public class TodoDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }

        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }
    }
}
