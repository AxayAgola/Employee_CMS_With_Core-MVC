using CMS23MCA.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMS23MCA.Controllers
{
    public class EmployeeController : Controller
    {


        EmployeeModel model = new EmployeeModel();
        public IActionResult Index()
        {
            List<EmployeeModel> lst = model.getData();
            return View(lst);
        }

        public IActionResult AddEmployee()
        {
            return View();

        }

        [HttpPost]
        public IActionResult AddEmployee(EmployeeModel emp)
        {
            bool res;
            if (ModelState.IsValid)
            {

                res = model.insert(emp);
                if (res)
                {
                    TempData["msg"] = "Added successfully";
                }
                else
                {
                    TempData["msg"] = "Not Added. something went wrong..!!";
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult DeleteEmployee(string Id)
        {
            EmployeeModel Emp = model.getData(Id);
            return View(Emp);
        }
        [HttpPost]
        public IActionResult DeleteEmployee(EmployeeModel Emp)
        {
            bool res;
            model = new EmployeeModel();
            res = model.delete(Emp);
            if (res)
            {
                TempData["msg"] = "Deleted successfully";
            }
            else
            {
                TempData["msg"] = "Not Deleted. something went wrong..!!";
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditEmployee(string id)
        {
            EmployeeModel emp = model.getData(id);
            return View(emp);
        }

        [HttpPost]
        public IActionResult EditEmployee(EmployeeModel emp)
        {
            bool res;
            if (ModelState.IsValid)
            {
                model = new EmployeeModel();
                res = model.update(emp);
                if (res)
                {
                    TempData["msg"] = "Updated successfully";
                }
                else
                {
                    TempData["msg"] = "Not Updated. something went wrong..!!";
                }
            }
            return View();
        }
    }
}
