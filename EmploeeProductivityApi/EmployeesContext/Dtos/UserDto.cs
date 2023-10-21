using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProductivity.Application.Dtos
{
    public class UserDto
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public bool IsDirector { get; set; }
        [Required]
        public string Department { get; set; } = null!;
    }
}
