using AutoMapper;
using EmployeeProductivity.Application.Entities;
using EmployeeProductivity.Infrustructure.RatingCreators;
using EmployeesContext;
using EmployeesContext.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProductivityApi.Controllers
{
    [Authorize(Roles = "Director")]
    [ApiController]
    [Route("api/[Controlller]")]
    public class RatingController : WebApiController
    {
        public RatingController(DataContext context, IMapper mapper, ScoreRequest request) : base(context, mapper) { }

        [HttpGet]
        [Route("/AllRating")]
        public IActionResult GetUsersRating()
        {
            Director director = _context.Directors.Where(e => e.Email == User.Identity.Name).FirstOrDefault();
            List<Employee> employees = _context.Employees.Where(e => e.DirectorId == director.Id).ToList();
            List<Score> scores = new List<Score>();

            foreach (Employee employee in employees)
                scores.Add(_context.Scores.Where(s => s.EmployeeId == employee.Id).FirstOrDefault());

            ScoreRequest request = new ScoreRequest(scores.ToArray());
            request.SeedScore();
            return Ok();
        }
    }
}
