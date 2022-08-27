using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChetuApplication12.Models;
using ChetuApplication12.Models.ViewModel;
using Microsoft.AspNetCore.Http;

namespace ChetuApplication12.Controllers
{
    public class NewEmployeeController : Controller
    {
        private readonly ApplicationContext context;

        public NewEmployeeController(ApplicationContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            //var emps = context.Employees.ToList();
            //var depts = context.Departments.ToList();

            //EmployeeDepartmentModel model = new EmployeeDepartmentModel();
            //model.Departments = depts;
            //model.Employees = emps;

            //return View(model);

            var emps = (from e in context.Employees
                        join
                        d in context.Departments
                        on e.Department.Id equals d.Id
                        select new EmployeeViewModel()
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Email = e.Email,
                            Gender = e.Gender,
                            Salary = e.Salary,
                            Department = d.Name

                        }).ToList();
            return View(emps);

        }
        public IActionResult Create()
        {
            var depts = context.Departments.ToList();
            ViewBag.depts = depts;
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            int id = model.Department;
            var dept = context.Departments.SingleOrDefault(e => e.Id == id);
            Employee emp = new Employee()
            {
                Name = model.Name,
                Email = model.Email,
                Gender = model.Gender,
                Salary = model.Salary,
                Department = dept
            };


            context.Employees.Add(emp);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var emp = context.Employees.SingleOrDefault(e => e.Id == id);
            if (emp != null)
            {
                context.Employees.Remove(emp);
                context.SaveChanges();
                TempData["error"] = "Employee deleted !";

                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Employee not found !";
                return RedirectToAction("Index");
            }
        }
        public IActionResult Update(int id)
        {
            var emp = context.Employees.SingleOrDefault(e => e.Id == id);
            if(emp != null)
            {
                var depts = context.Departments.ToList();
                ViewBag.depts = depts;
                var model = new EmployeeCreateViewModel()
                {
                    Id=emp.Id,
                    Name=emp.Name,
                    Email=emp.Email,
                    Salary=emp.Salary,
                    Gender=emp.Gender,
                    Department=emp.Department.Id
                };
                return View(model);
            }
            else
            {
                TempData["error"] = "Employee not found";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Update(EmployeeCreateViewModel model)
        {
            int id = model.Department;
            var dept = context.Departments.SingleOrDefault(e => e.Id == id);
            Employee emp = new Employee()
            {
                Id=model.Id,
                Name = model.Name,
                Email = model.Email,
                Gender = model.Gender,
                Salary = model.Salary,
                Department = dept
            };


            context.Employees.Update(emp);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult CreateDepartment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateDepartment(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();
            return RedirectToAction("ShowDepartments");

        }
        public IActionResult ShowDepartments()
        {
            return View(context.Departments.ToList());
        }
        public IActionResult DeleteDepartment(int id)
        {
            var dept = context.Departments.SingleOrDefault(e => e.Id == id);
            if (dept != null)
            {
                if (context.Employees.Any(e => e.Department == dept))
                {
                    TempData["error"] = "Department already used by any employee !";
                    return RedirectToAction("ShowDepartments");
                }
                else
                {
                    context.Departments.Remove(dept);
                    context.SaveChanges();
                    TempData["error"] = "Department deleted !";
                    return RedirectToAction("ShowDepartments");

                }


            }
            else
            {
                TempData["error"] = "Department not found !";
                return RedirectToAction("ShowDepartments");
            }
        }

    }

}
