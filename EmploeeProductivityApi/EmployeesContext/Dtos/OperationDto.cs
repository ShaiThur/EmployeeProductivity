using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesContext.Dtos
{
    public class OperationDto
    {
        public string OperationName { get; set; } = null!;
        public int ScoreSum { get; set; }
        public string OperationDescription { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public DateTime Deadline { get; set; }
    }
}
