using AutoMapper;
using EmployeeProductivity.Application.Entities;
using EmployeesContext;
using EmployeesContext.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProductivityApi.Controllers
{
    [Authorize(Roles = "Director")]
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : WebApiController
    {
        public OperationsController(DataContext context, IMapper mapper) : base(context, mapper) { }

        [HttpGet]
        [Route("/allTasks")]
        public IActionResult ShowAllOperations()
        {
            Director user = _context.Directors.Where(e => e.Email == User.Identity.Name).FirstOrDefault();
            if (user == null) return NotFound();
            List<Operation> ops = _context.Operations.Where(o => o.DirectorId == user.Id).ToList();
            List<OperationDto> opsDto = new List<OperationDto>();

            foreach (Operation op in ops)
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
        public IActionResult AddOperation(OperationDto dto)
        {
            Operation operation = _context.Operations.Where(o => o.OperationName == dto.OperationName).FirstOrDefault();
            if (operation == null) return BadRequest();
            operation.Description = dto.OperationDescription;
            operation.Deadline = dto.Deadline;
            _context.Operations.Update(operation);
            _context.SaveChanges();
            return Ok();
        }
    }
}
