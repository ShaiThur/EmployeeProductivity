using EmployeeProductivity.Application.Entities;
using EmployeesContext.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesContext.Repositories
{
    public class OperationRepository : IRepository<Operation>
    {
        DataContext _context;
        public OperationRepository(DataContext context)
        {
            _context = context;
        }
        public bool Create(Operation entity)
        {
            _context.Operations.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            bool result = true;
            try
            {
                _context.Operations.Remove(_context.Operations.Where(e => e.Id == id).FirstOrDefault());
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

        public IEnumerable<Operation> GetAll()
        {
            var employees = _context.Set<Operation>().ToList();
            return employees;
        }

        public Operation GetById(int id)
        {
            var employee = _context.Operations.Where(x => x.Id == id).FirstOrDefault();
            return employee;
        }

        public bool Update(Operation entity)
        {
            var employee = _context.Operations.Where(x => x.Id == entity.Id).FirstOrDefault();
            if (employee != null)
            {
                employee.OperationName = entity.OperationName;
                employee.Description = entity.Description;
                employee.Validity = entity.Validity;
                employee.Deadline = entity.Deadline;

                _context.SaveChanges();
                return true;
            }
            return false;
        }

        bool IRepository<Operation>.DeleteAll()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Operation> IRepository<Operation>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
