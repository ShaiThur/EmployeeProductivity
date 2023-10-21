using EmployeeProductivity.Application.Entities;
using EmployeesContext;
using EmployeesContext.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
