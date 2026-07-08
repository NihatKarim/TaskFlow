using Microsoft.EntityFrameworkCore;
using Task_Web_Application.Models;

namespace Task_Web_Application.Data
{
    public class AppDatabase : DbContext
    {
        public AppDatabase(DbContextOptions<AppDatabase> options)
            : base(options)
        {
        }

        public DbSet<RegisterModel> registerModels { get; set; }
        public DbSet<TaskHistory> TaskHistories { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
    }
}