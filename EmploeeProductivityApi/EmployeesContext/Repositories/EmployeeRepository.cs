using EmployeeProductivity.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesContext.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        DataContext _context;
        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }
        public bool Create(Employee entity)
        {
            _context.Employees.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            bool result = true;
            try
            {
                _context.Employees.Remove(_context.Employees.Where(e => e.Id == id).FirstOrDefault());
            }
            catch (NullReferenceException)
            {
                result = false;
            }
            return result;
        }

        public void DeleteAll()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE [TableName]");
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = _context.Set<Employee>().ToList();
            return employees;
        }

        public Employee GetById(int id)
        {
            var employee = _context.Employees.Where(x => x.Id == id).FirstOrDefault();
            return employee;
        }

        public bool Update(Employee entity)
        {
            var employee = _context.Employees.Where(x => x.Email == entity.Email).FirstOrDefault();
            if (employee != null)
            {
                employee.Email = entity.Email;
                employee.FirstName = entity.FirstName;
                employee.LastName = entity.LastName;
                employee.Password = entity.Password;

                _context.SaveChanges();
                return true;
            }
            return false;
        }

        bool IRepository<Employee>.DeleteAll()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Employee> IRepository<Employee>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
