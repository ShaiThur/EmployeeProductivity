using AutoMapper;
using EmployeeProductivity.Application.Entities;
using EmployeesContext;
using EmployeesContext.Dtos;
using EmployeesContext.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeProductivityApi.Controllers
{
    [Authorize(Roles = "Employee")]
    [Route("api/[Controller]")]
    [ApiController]
    public class EmployeeController : WebApiController
    {
        delegate void EmployeeHandler(Operation operation);
        event EmployeeHandler Notify;

        public EmployeeController(DataContext context, IMapper mapper) : base(context, mapper) { }
        [HttpGet]
        [Route("/myTasks")]
        public IActionResult GetOperations()
        {
            Employee employee = _context.Employees.Where(e => e.Email == User.Identity.Name).FirstOrDefault();
            Director director = _context.Directors.Where(d => d.Id == employee.DirectorId).FirstOrDefault();
            List<Operation> ops = _context.Set<Operation>().ToList();
            List<OperationDto> opsDto = new List<OperationDto>();

            foreach (Operation op in ops)
            {
                if (op.DirectorId == director.Id && op.IsAccessed == true)
                    opsDto.Add(_mapper.Map<OperationDto>(op));
            }
            return Ok(opsDto);
        }

        [HttpPost]
        [Route("/startSolving")]
        public IActionResult StartSolvingOperation([FromBody] OperationDto operation)
        {
            Employee employee = _context.Employees.Where(e => e.Email == User.Identity.Name).FirstOrDefault();
            Score score = _context.Scores.Where(sc => sc.EmployeeId == employee.Id).FirstOrDefault();
            Operation op = _mapper.Map<Operation>(operation);
            Notify.Invoke(op);
            return Ok();
        }
    }
}
