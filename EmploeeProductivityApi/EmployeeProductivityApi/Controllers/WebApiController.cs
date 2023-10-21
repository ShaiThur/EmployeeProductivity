using AutoMapper;
using EmployeesContext;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProductivityApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class WebApiController : ControllerBase
    {
        protected readonly DataContext _context;
        protected readonly IMapper _mapper;
        public WebApiController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
