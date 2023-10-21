using EmployeesContext;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProductivityApi.Controllers
{
    [ApiController]
    public class WebApiController : ControllerBase
    {
        protected readonly DataContext _context;
        public WebApiController(DataContext context)
        {
            _context = context;
        }

    }
}
