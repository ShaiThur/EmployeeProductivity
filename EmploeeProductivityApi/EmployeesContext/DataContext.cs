using EmployeeProductivity.Application.Entities;
using EmployeesContext.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();   
        }
        public DbSet<Director>  Directors{ get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<UnregisteredEmployee> UnregisteredEmployees { get; set; }
    }
}
