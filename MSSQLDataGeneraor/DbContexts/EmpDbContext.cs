using MSSQLDataGenerator.Models;
using Microsoft.EntityFrameworkCore;

namespace MSSQLDataGenerator.DbContexts
{
    public class EmpDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Location> locations { get; set; }
        public DbSet<PayScale> PayScales { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=.; initial catalog=EmployeeTestDB;Integrated Security=True;TrustServerCertificate=True;user id=sa");
        }
    }
}
