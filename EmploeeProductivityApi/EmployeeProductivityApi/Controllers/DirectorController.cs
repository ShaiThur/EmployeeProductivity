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

        [HttpGet]
        [Route("/allTasks")]
        public IActionResult ShowAllOperations()
        {
            Director user = _context.Directors.Where(e => e.Email == User.Identity.Name).FirstOrDefault();
            if (user == null) return NotFound();
            List<Operation> ops = _context.Operations.Where(o => o.DirectorId == user.Id).ToList();
            List<OperationDto> opsDto = new List<OperationDto>();

            foreach(Operation op in ops)
            {
                opsDto.Add(_mapper.Map<OperationDto>(op));
            }
            return Ok(ops);
        }

        [HttpPost]
        [Route("/createTask")]
        public IActionResult CreateOperation(OperationDto dto)
        {
            if (dto == null) return BadRequest(ModelState);

            Director director = _context.Directors.Where(e => e.Email == User.Identity.Name).FirstOrDefault();
            if (director == null) return BadRequest(ModelState);

            Operation operation = _mapper.Map<Operation>(dto);
            operation.Id = _context.Set<Operation>().Count() + 1;
            operation.DirectorId = director.Id;
            operation.Description = dto.OperationDescription;
            _context.Operations.Add(operation);
            _context.SaveChanges();
            return Ok(operation);
        }

        [HttpDelete]
        [Route("/deliteTask")]
        public IActionResult DeleteOperation(string OperationName)
        {
            if (OperationName == null) return BadRequest(ModelState);
            Operation operation = _context.Operations.Where(o => o.OperationName == OperationName).FirstOrDefault();
            _context.Operations.Remove(operation);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("/updateTask")]
        public IActionResult PutOperation(OperationDto dto)
        {
            Operation operation = _context.Operations.Where(o => o.OperationName == dto.OperationName).FirstOrDefault();
            if (operation == null) return BadRequest();
            operation.Description = dto.OperationDescription;
            operation.Deadline = dto.Deadline;
            _context.Operations.Update(operation);
            _context.SaveChanges();
            return Ok();
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

        [HttpGet]
        [Route("/showUnregistered")]
        public IActionResult GetNewEmployees()
        {
            IEnumerable<Unregistered> unregistereds = _context.Set<Unregistered>().ToList();
            if (unregistereds.Any() || unregistereds.Count() == 0) return NotFound(ModelState);
            return Ok(unregistereds);
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
