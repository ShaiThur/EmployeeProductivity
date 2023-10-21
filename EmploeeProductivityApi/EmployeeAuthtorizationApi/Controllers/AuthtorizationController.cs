using AutoMapper;
using EmployeeAuthtorizationApi.Controllers.Extentions;
using EmployeeProductivity.Application.Dtos;
using EmployeeProductivity.Application.Entities;
using EmployeesContext;
using EmployeesContext.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace EmployeeAuthtorizationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthtorizationController : WebApiController
    {
        public AuthtorizationController(DataContext context, IConfiguration configuration) : base(context, configuration) { }

        [HttpPost("/reg")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto user)
        {
            if (user == null)
                return BadRequest(ModelState);
            string token = string.Empty;
            if (user.IsDirector)
                token = await CreateDirectorAsync(user);
            else
                token = await CreateUnregisteredAsync(user);

            return Ok(token);
        }

        [HttpPost("/auth")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult AuthUser([FromBody] UserInfo user)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var directors = _context.Set<Director>().ToList();
            var employees = _context.Set<Employee>().ToList();
            Director director = CheckUser(user, directors);
            Employee employee = CheckUser(user, employees);
            if (director == null && employee == null)
            {
                return BadRequest("Bad credentials");
            }
            string newToken = string.Empty;

            if (user is null)
                return Unauthorized();

            if (director != null)
            {
                newToken = Encryptor.GenerateJwtToken(director.Email, director.Role, _configuration);
                director.Token = newToken;
                _context.Directors.Update(director);
            }
            else
            {
                newToken = Encryptor.GenerateJwtToken(employee.Email, employee.Role, _configuration);
                employee.Token = newToken;
                _context.Employees.Update(employee);
            }

            _context.SaveChanges();
            return Ok(new { Token = newToken });
        }

        private async Task<string> CreateDirectorAsync(UserDto user)
        {
            {
                int depId = 0;
                if (!_context.Departments.Any(d => d.DepartmentName.ToLower() == user.Department.ToLower()))
                {
                    depId = _context.Set<Department>().Count() + 1;
                    _context.Departments.Add(new Department { DepartmentName = user.Department, Id = depId });
                }
                else depId = _context.Departments.Where(d => d.DepartmentName.ToLower() == user.Department.ToLower()).First().Id;
                Director director = new Director
                {
                    Id = _context.Set<Director>().Count() + 1,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = Encryptor.EncodePasswordToBase64(user.Password),
                    Token = Encryptor.GenerateJwtToken(user.Email, new Director().Role, _configuration),
                    Email = user.Email,
                    DepartmentId = depId
                };
                await Task.Run(() =>
                {
                    _context.Directors.Add(director);
                    _context.SaveChanges();
                });

                return director.Token;
            }
        }
        private async Task<string> CreateUnregisteredAsync(UserDto user)
        {
            int depId = _context.Departments.Where(d => d.DepartmentName.ToLower() == user.Department.ToLower()).FirstOrDefault().Id;
            Unregistered userEmployee = new Unregistered
            {
                Id = _context.Set<User>().Count() + 1,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = Encryptor.EncodePasswordToBase64(user.Password),
                Token = Encryptor.GenerateJwtToken(user.Email, new Employee().Role, _configuration),
                Email = user.Email,
                DepartmentId = depId
            };

            await Task.Run(() =>
            {
                _context.UnregisteredEmployees.Add(userEmployee);
                _context.SaveChanges();
            });

            return userEmployee.Token;
        }
        private Director CheckUser(UserInfo user, List<Director> users)
        {
            Director goodUser = null;
            foreach (var e in users)
            {
                if (e.Email == user.email && Encryptor.DecodeFrom64(e.Password) == user.password)
                {
                    goodUser = e;
                    break;
                }
            }
            return goodUser;
        }
        private Employee CheckUser(UserInfo user, List<Employee> users)
        {
            Employee goodUser = null;
            foreach (var e in users)
            {
                if (e.Email == user.email && Encryptor.DecodeFrom64(e.Password) == user.password)
                {
                    goodUser = e;
                    break;
                }
            }
            return goodUser;
        }
    }
    public record UserInfo(string email, string password);
}
