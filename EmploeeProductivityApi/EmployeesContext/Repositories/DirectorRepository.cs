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
    public class DirectorRepository : IRepository<Director>
    {
        DataContext _context;
        public DirectorRepository(DataContext context)
        {
            _context = context;
        }
        public bool Create(Director entity)
        {
            _context.Directors.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            bool result = true;
            try
            {
                _context.Directors.Remove(_context.Directors.Where(e => e.Id == id).FirstOrDefault());
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

        public IEnumerable<Director> GetAll()
        {
            var directors = _context.Set<Director>().ToList();
            return directors;
        }

        public Director GetById(int id)
        {
            var director = _context.Directors.Where(x => x.Id == id).FirstOrDefault();
            return director;
        }

        public bool Update(Director entity)
        {
            var director = _context.Directors.Where(x => x.Email == entity.Email).FirstOrDefault();
            if (director != null)
            {
                director.Email = entity.Email;
                director.FirstName = entity.FirstName;
                director.LastName = entity.LastName;
                director.Password = entity.Password;

                _context.SaveChanges();
                return true;
            }
            return false;
        }

        bool IRepository<Director>.DeleteAll()
        {
            throw new NotImplementedException();
        }
    }
}
