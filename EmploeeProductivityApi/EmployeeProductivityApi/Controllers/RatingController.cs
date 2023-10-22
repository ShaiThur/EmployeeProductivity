using AutoMapper;
using EmployeeProductivity.Application.Entities;
using EmployeeProductivity.Infrustructure.RatingCreators;
using EmployeesContext;
using EmployeesContext.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProductivityApi.Controllers
{
    #if !DEBUG
    [Authorize(Roles = "Director")]
    #endif
    [ApiController]
    [Route("api/[Controlller]")]
    public class RatingController : ControllerBase
    {
        ScoreRequest request;
        Dictionary<int, float> values = new Dictionary<int, float>();

        public RatingController()
        {
            request = new ScoreRequest(SeedScore().ToArray());
        }

        [HttpGet]
        [Route("/AllRating")]
        public JsonResult GetUsersRating()
        {
            //Director director = _context.Directors.Where(e => e.Email == User.Identity.Name).FirstOrDefault();
            //List<Employee> employees = _context.Employees.Where(e => e.DirectorId == director.Id).ToList();
            //List<Score> scores = new List<Score>();

            //foreach (Employee employee in employees)
            //    scores.Add(_context.Scores.Where(s => s.EmployeeId == employee.Id).FirstOrDefault());
            for (int i = 1; i <= 100; i++)
            {
                values.Add(i, request.GetEmployeesRating(i));
            }
            return new JsonResult(values);
        }

        [HttpGet]
        [Route("/MonthRating")]
        public IActionResult GetUsersRatingByMonth()
        {
            for (int i = 1; i <= 100; i++)
            {
                values.Add(i, request.GetScoreOfEmployeesMonth(new[] {i}));
            }
            return Ok(values);
        }

        [HttpGet]
        [Route("/WeekRating")]
        public IActionResult GetScoreByWeek()
        {
            for (int i = 1; i <= 100; i++)
            {
                values.Add(i, request.GetScoreOfEmployeesWeek(new[] {i} ));
            }
            return Ok(values);
        }

        [HttpGet]
        [Route(("/DayRating"))]
        public IActionResult GetScoreByDay()
        {
            for (int i = 1; i <= 100; i++)
            {
                values.Add(i, request.GetScoreOfEmployeesDay(new[] {i} ));
            }
            return Ok(values);
        }

        private static List<Score> SeedScore()
        {
            var scores = new List<Score>();
            Random rnd = new Random();
            for (int i = 1; i <= 100; i++)
            {
                scores.Add(
                    new Score
                    {
                        TotalScore = new Random().Next(1, 1000),
                        DayScore = rnd.Next(1, 5),
                        WeekScore = rnd.Next(1, 23),
                        MonthScore = rnd.Next(1, 454),
                        EmployeeId = i,
                        ScoreId = i
                    }
                );
            }
            return scores;
        }
    }
}
