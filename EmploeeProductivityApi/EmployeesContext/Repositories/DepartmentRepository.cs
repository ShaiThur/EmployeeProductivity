using EmployeeProductivity.Application.Entities;
using EmployeesContext;
using EmployeesContext.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesContext.Repositories
{
    public class DepartmentRepository : IRepository<Department>
    {
        DataContext _context;
        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }
        public bool Create(Department entity)
        {
            _context.Departments.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            bool result = true;
            try
            {
                _context.Departments.Remove(_context.Departments.Where(e => e.Id == id).FirstOrDefault());
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

        public IEnumerable<Department> GetAll()
        {
            var departments = _context.Set<Department>().ToList();
            return departments;
        }

        public Department GetById(int id)
        {
            var department = _context.Departments.Where(x => x.Id == id).FirstOrDefault();
            return department;
        }

        public bool Update(Department entity)
        {
            Department department = _context.Departments.Where(x => x.Id == entity.Id).FirstOrDefault();
            if (department != null)
            {
                department.DepartmentName = entity.DepartmentName;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        bool IRepository<Department>.DeleteAll()
        {
            throw new NotImplementedException();
        }
    }
}
