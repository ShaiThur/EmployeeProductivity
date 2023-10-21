using EmployeesContext;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAuthtorizationApi.Controllers
{
    [ApiController]
    public class WebApiController : ControllerBase
    {
        protected readonly IConfiguration _configuration;
        protected readonly DataContext _context;
        public WebApiController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

    }
}
