using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChetuApplication12.Models.ViewModel
{
    public class EmployeeCreateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public string Gender { get; set; }
        public int Department { get; set; }
    }
}
