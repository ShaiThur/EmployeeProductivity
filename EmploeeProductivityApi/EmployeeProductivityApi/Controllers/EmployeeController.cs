using AutoMapper;
using EmployeeProductivity.Application.Entities;
using EmployeesContext;
using EmployeesContext.Dtos;
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
        private readonly IMapper _mapper;
        public EmployeeController(DataContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        [HttpGet]
        [Route("/myTasks")]
        public IActionResult ShowOperations()
        {
            Employee employee = _context.Employees.Where(e => e.Email == User.Identity.Name).FirstOrDefault();
            Director? director = _context.Directors.Where(d => d.Id == employee.DirectorId).FirstOrDefault();
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
        [Route("/solve")]
        public IActionResult SolveTask()
        {
            Employee employee = _context.Employees.Where(e => e.Email == User.Identity.Name).FirstOrDefault();
            employee.Score++;
            _context.Employees.Update(employee);
            _context.SaveChanges();

            return Ok();
        }
    }
}
