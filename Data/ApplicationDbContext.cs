using Microsoft.EntityFrameworkCore;
using TodoList1.Models;

namespace TodoList1.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<TasksModel> Tasks { get; set; }
    }
}
