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
        public string OperationDescription { get; set; } = null!;
        public DayOfWeek Validity { get; set; }
        public DateTime Deadline { get; set; }
    }
}
