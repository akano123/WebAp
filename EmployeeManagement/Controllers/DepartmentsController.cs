using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class DepartmentsController : Controller
    {

        EmployeeDbContext context = new EmployeeDbContext();
        // GET: Departments
        public ActionResult Index()
        {

            return View(context.Departments.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Department department)
        {
            if (department == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    context.Departments.Add(department);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Departments", null);
                }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return View(department);
        }

        [HttpPost]
        public JsonResult CreateByAjax(Department department)
        {
            bool createSuccess = false;
            try
            {
                if (ModelState.IsValid)
                {
                    context.Departments.Add(department);
                    context.SaveChanges();
                    createSuccess = true;
                }
            }
            catch (Exception)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return Json(new
            {
                Status = createSuccess,
                Result = department
            });
        }
    }
}