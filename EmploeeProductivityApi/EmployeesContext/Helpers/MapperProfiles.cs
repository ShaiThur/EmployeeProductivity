using AutoMapper;
using EmployeeProductivity.Application.Dtos;
using EmployeeProductivity.Application.Entities;
using EmployeesContext.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesContext.Helpers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<Operation, OperationDto>();
            CreateMap<Department, DepartmentDto>();
        }
    }
}
