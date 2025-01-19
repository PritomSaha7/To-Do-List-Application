using Microsoft.EntityFrameworkCore;
using ToDoListApplication.Models.Entities;

namespace ToDoListApplication.Data;


    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        
        }
    
        public DbSet<WorkTable> works { get; set; }
    }