using AutoMapper;
using EmployeeProductivity.Application.Dtos;
using EmployeeProductivity.Application.Entities;
using EmployeesContext;
using EmployeesContext.Dtos;
using EmployeesContext.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace EmployeeProductivityApi.Controllers
{
    [Authorize(Roles = "Director")]
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : WebApiController
    {
        public DirectorController(DataContext context, IMapper mapper) : base(context, mapper) { }


        [HttpPut]
        [Route("/addAllEmployees")]
        public IActionResult AddAllEmployees()
        {
            Director director = _context.Directors.Where(d => d.Email == User.Identity.Name).FirstOrDefault();

            List<Unregistered> unregisteredEmployees = _context.Set<Unregistered>().ToList();
            foreach (Unregistered user in unregisteredEmployees)
            {
                AddEmployeeInDb(user, director);
            }
            return Ok();
        }

        [HttpPut]
        [Route("/addEmployee")]
        public IActionResult AddEmployee(UserDto user)
        {
            Director director = _context.Directors.Where(d => d.Email == User.Identity.Name).FirstOrDefault();
            Unregistered employee = _context.UnregisteredEmployees.Where(e => e.Email == user.Email).FirstOrDefault();
            AddEmployeeInDb(employee, director);
            return Ok();
        }

        [HttpGet]
        [Route("/showUnregistered")]
        public IActionResult ShowNewEmployees()
        {
            IEnumerable<Unregistered> unregistereds = _context.Set<Unregistered>().ToList();
            if (unregistereds.Any() || unregistereds.Count() == 0) return NotFound(ModelState);
            return Ok(unregistereds);
        }

        [HttpGet]
        [Route("/showAllEmployee")]
        public IActionResult ShowAllEmployees()
        {
            Director director = _context.Directors.Where(d => d.Email == User.Identity.Name).FirstOrDefault();
            return Ok(_context.Employees.Where(e => e.DepartmentId == director.DepartmentId).ToList());
        }

        private void AddEmployeeInDb(Unregistered user, Director director)
        {
            Employee employee = (User)user as Employee;
            employee.DirectorId = director.Id;
            Score score = new Score { EmployeeId = employee.Id };
            _context.Employees.Add(employee);
            _context.Scores.Add(score);
            _context.SaveChanges();
        }
    }
}
