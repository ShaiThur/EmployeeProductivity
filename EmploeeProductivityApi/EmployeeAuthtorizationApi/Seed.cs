using EmployeeAuthtorizationApi.Controllers.Extentions;
using EmployeeProductivity.Application.Entities;
using EmployeesContext;

namespace EmployeeAuthtorizationApi
{
    public class Seed
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public Seed(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public void CreateSeed()
        {
            Director director = new Director
            {
                Id = 1,
                DepartmentId = 1,
                Email = "D1",
                FirstName = "Test",
                LastName = "Test",
                Password = Encryptor.EncodePasswordToBase64("Test"),
                Token = Encryptor.GenerateJwtToken("D1", "Director", _configuration)
            };
            Employee employee1 = new Employee
            {
                Id = 1,
                DepartmentId = 1,
                DirectorId = 1,
                Email = "E1",
                FirstName = "Test",
                LastName = "Test",
                Password = Encryptor.EncodePasswordToBase64("Test"),
                Token = Encryptor.GenerateJwtToken("E1", "Director", _configuration)
            };
            Employee employee2 = new Employee
            {
                Id = 2,
                DepartmentId = 1,
                DirectorId = 1,
                Email = "E2",
                FirstName = "Test1",
                LastName = "Test1",
                Password = Encryptor.EncodePasswordToBase64("Test"),
                Token = Encryptor.GenerateJwtToken("E2", "Director", _configuration)
            };
            Operation operation1 = new Operation
            {
                Id = 1,
                Deadline = DateTime.UtcNow.AddDays(1),
                IsAccessed = false,
                Validity = DayOfWeek.Saturday,
                DirectorId = 1,
                Description = "Test1",
                OperationName = "Test1",
            };
            Operation operation2 = new Operation
            {
                Id = 2,
                Deadline = DateTime.UtcNow.AddDays(1),
                IsAccessed = false,
                Validity = DayOfWeek.Saturday,
                DirectorId = 1,
                Description = "Test2",
                OperationName = "Test2",
            };
            Operation operation3 = new Operation
            {
                Id = 3,
                Deadline = DateTime.UtcNow.AddDays(1),
                IsAccessed = false,
                Validity = DayOfWeek.Saturday,
                DirectorId = 1,
                Description = "Test3",
                OperationName = "Test3",
            };
            Department department = new Department
            {
                Id = 1,
                DepartmentName = "TestDep",
            };
            _context.Directors.Add(director);
            _context.Employees.Add(employee1);
            _context.Employees.Add(employee2);
            _context.Operations.Add(operation1);
            _context.Operations.Add(operation2);
            _context.Operations.Add(operation3);
            _context.Departments.Add(department);
            _context.SaveChanges();
        }
    }
}
