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
        public DirectorController(DataContext context) : base(context) { }

        [HttpGet]
        [Route("/allTasks")]
        public IActionResult ShowAllOperations()
        {
            User user = _context.Directors.Where(e => e.Email == User.Identity.Name).First();
            List<Operation> ops = _context.Operations.Where(o => o.DirectorId == user.Id).ToList();
            return Ok(ops);
        }

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

        [NonAction]
        public void AddEmployeeInDb(Unregistered user, Director director)
        {
            Employee employee = (User)user as Employee;
            employee.DirectorId = director.Id;
            Score score = new Score { ScoreId = employee.Id };
            employee.ScoreId = score.ScoreId;
            _context.Employees.Add(employee);
            _context.Scores.Add(score);
            _context.SaveChanges();
        }
    }
}
