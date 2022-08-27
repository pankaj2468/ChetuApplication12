using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChetuApplication12.Models;

namespace ChetuApplication12.Controllers
{
    
    public class EmployeeController : Controller
    {
      private  ApplicationContext context;
        public EmployeeController(ApplicationContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            var depts = context.Departments.ToList();
            return View(depts);
        }
        public IActionResult GetEmployee(int id)
        {
            var emps = context.Employees.Where(e => e.Department.Id == id);
            return View(emps);
        }
        public IActionResult EmployeeDetail(int id)
        {
            var emp = context.Employees.SingleOrDefault(e => e.Id == id);
            return View(emp);
        }
    }
}
