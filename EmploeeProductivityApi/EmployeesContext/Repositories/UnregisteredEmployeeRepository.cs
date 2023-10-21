using EmployeesContext.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesContext.Repositories
{
    public class UnregisteredEmployeeRepository : IRepository<UnregisteredEmployee>
    {
        DataContext _context;
        public UnregisteredEmployeeRepository(DataContext context)
        {
            _context = context;
        }
        public bool Create(UnregisteredEmployee entity)
        {
            _context.UnregisteredEmployees.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            bool result = true;
            try
            {
                _context.UnregisteredEmployees.Remove(_context.UnregisteredEmployees.Where(e => e.Id == id).FirstOrDefault());
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

        public IEnumerable<UnregisteredEmployee> GetAll()
        {
            var employees = _context.Set<UnregisteredEmployee>().ToList();
            return employees;
        }

        public UnregisteredEmployee GetById(int id)
        {
            var employee = _context.UnregisteredEmployees.Where(x => x.Id == id).FirstOrDefault();
            return employee;
        }

        public bool Update(UnregisteredEmployee entity)
        {
            var employee = _context.UnregisteredEmployees.Where(x => x.Email == entity.Email).FirstOrDefault();
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

        bool IRepository<UnregisteredEmployee>.DeleteAll()
        {
            throw new NotImplementedException();
        }
    }
}
