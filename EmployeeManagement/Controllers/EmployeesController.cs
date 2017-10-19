using PagedList;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement.ViewModels;
using System.Net;
using System.Transactions;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        EmployeeDbContext context = new EmployeeDbContext();

        #region index demo
        public ActionResult Index2(string searchValue, string sortField, int? pageNumber)
        {
            //List<Employee> employees = context.Employees.Include("Department").ToList();
            //List<Employee> employees = context.Employees.Include(x => x.Department).ToList();

            IQueryable<Employee> query = context.Employees
                                                .Include(x => x.Department)
                                                .Where(x => searchValue == null
                                                         || x.FirstName.Contains(searchValue)
                                                         || x.LastName.Contains(searchValue));

            ViewBag.SortByName = string.IsNullOrEmpty(sortField) ? "Name_Desc" : string.Empty;
            ViewBag.SortByGender = sortField == "Gender_Asc" ? "Gender_Desc" : "Gender_Asc";
            ViewBag.SortByBirthdate = sortField == "Birthdate_Asc" ? "Birthdate_Desc" : "Birthdate_Asc";
            ViewBag.SortByMarriedStatus = sortField == "MarriedStatus_Asc" ? "MarriedStatus_Desc" : "MarriedStatus_Asc";
            ViewBag.SortByEmail = sortField == "Email_Asc" ? "Email_Desc" : "Email_Asc";
            ViewBag.SortBySalary = sortField == "Salary_Asc" ? "Salary_Desc" : "Salary_Asc";
            ViewBag.SortByDepartment = sortField == "Department_Asc" ? "Department_Desc" : "Department_Asc";

            switch (sortField)
            {
                case "Name_Desc": query = query.OrderByDescending(x => x.FirstName); break;
                case "Gender_Asc": query = query.OrderBy(x => x.Gender); break;
                case "Gender_Desc": query = query.OrderByDescending(x => x.Gender); break;
                case "Birthdate_Asc": query = query.OrderBy(x => x.DateOfBirth); break;
                case "Birthdate_Desc": query = query.OrderByDescending(x => x.DateOfBirth); break;
                case "MarriedStatus_Asc": query = query.OrderBy(x => x.IsMarried); break;
                case "MarriedStatus_Desc": query = query.OrderByDescending(x => x.IsMarried); break;
                case "Email_Asc": query = query.OrderBy(x => x.Email); break;
                case "Email_Desc": query = query.OrderByDescending(x => x.Email); break;
                case "Salary_Asc": query = query.OrderBy(x => x.Salary); break;
                case "Salary_Desc": query = query.OrderByDescending(x => x.Salary); break;
                case "Department_Asc": query = query.OrderBy(x => x.Department.Name); break;
                case "Department_Desc": query = query.OrderByDescending(x => x.Department.Name); break;
                default: query = query.OrderBy(x => x.FirstName); break; //Name_Asc <=> sortField = null, ""
            }

            //List <Employee> list = query.ToList();
            IPagedList<Employee> list = query.ToPagedList<Employee>(pageNumber ?? 1, 10);
            ViewBag.CurrentEmployees = list.Count;
            ViewData["CurrentEmployees"] = list.Count;
            return View(list);
        }

        public ActionResult Index(string searchValue, string sortField, int? pageNumber)
        {
            //List<Employee> employees = context.Employees.Include("Department").ToList();
            //List<Employee> employees = context.Employees.Include(x => x.Department).ToList();

            IQueryable<EmployeeListVM> query = context.Employees
                                                .Include(x => x.Department)
                                                .Where(x => searchValue == null
                                                         || x.FirstName.Contains(searchValue)
                                                         || x.LastName.Contains(searchValue))
                                                .Select(x => new EmployeeListVM
                                                {
                                                    EmployeeId = x.EmployeeId,
                                                    FirstName = x.FirstName,
                                                    LastName = x.LastName,
                                                    DateOfBirth = x.DateOfBirth,
                                                    Gender = x.Gender,
                                                    Email = x.Email,
                                                    IsMarried = x.IsMarried,
                                                    Salary = x.Salary,
                                                    Skills = x.Skills,
                                                    DepartmentName = x.Department.Name
                                                });


            ViewBag.SortByName = string.IsNullOrEmpty(sortField) ? "Name_Desc" : string.Empty;
            ViewBag.SortByGender = sortField == "Gender_Asc" ? "Gender_Desc" : "Gender_Asc";
            ViewBag.SortByBirthdate = sortField == "Birthdate_Asc" ? "Birthdate_Desc" : "Birthdate_Asc";
            ViewBag.SortByMarriedStatus = sortField == "MarriedStatus_Asc" ? "MarriedStatus_Desc" : "MarriedStatus_Asc";
            ViewBag.SortByEmail = sortField == "Email_Asc" ? "Email_Desc" : "Email_Asc";
            ViewBag.SortBySalary = sortField == "Salary_Asc" ? "Salary_Desc" : "Salary_Asc";
            ViewBag.SortByDepartment = sortField == "Department_Asc" ? "Department_Desc" : "Department_Asc";

            switch (sortField)
            {
                case "Name_Desc": query = query.OrderByDescending(x => x.FirstName); break;
                case "Gender_Asc": query = query.OrderBy(x => x.Gender); break;
                case "Gender_Desc": query = query.OrderByDescending(x => x.Gender); break;
                case "Birthdate_Asc": query = query.OrderBy(x => x.DateOfBirth); break;
                case "Birthdate_Desc": query = query.OrderByDescending(x => x.DateOfBirth); break;
                case "MarriedStatus_Asc": query = query.OrderBy(x => x.IsMarried); break;
                case "MarriedStatus_Desc": query = query.OrderByDescending(x => x.IsMarried); break;
                case "Email_Asc": query = query.OrderBy(x => x.Email); break;
                case "Email_Desc": query = query.OrderByDescending(x => x.Email); break;
                case "Salary_Asc": query = query.OrderBy(x => x.Salary); break;
                case "Salary_Desc": query = query.OrderByDescending(x => x.Salary); break;
                case "Department_Asc": query = query.OrderBy(x => x.DepartmentName); break;
                case "Department_Desc": query = query.OrderByDescending(x => x.DepartmentName); break;
                default: query = query.OrderBy(x => x.FirstName); break; //Name_Asc <=> sortField = null, ""
            }

            //List <Employee> list = query.ToList();
            IPagedList<EmployeeListVM> list = query.ToPagedList(pageNumber ?? 1, 10);
            ViewBag.CurrentEmployees = list.TotalItemCount;
            ViewData["CurrentEmployees"] = list.TotalItemCount;
            ViewBag.PageCount = list.PageCount;
            return View(list);
        }
        #endregion

        public ActionResult Testing1()
        {
            List<string> jobs = new List<string>() { "Teacher", "IT", "Student" };
            return View(jobs);
        }
        public ActionResult Testing2()
        {
            List<string> jobs = new List<string>() { "Teacher", "IT", "Student" };
            ViewBag.Jobs = jobs;
            ViewData["Jobs"] = jobs;
            return View("~/Views/EmployeesPlugin/Testing2.cshtml");
        }
        #region Create - Model Binding
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Skills = this.GetSkills();
            ViewBag.Departments = this.GetDepartment();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            try
            {
                bool hasIdCard = context.Employees.Any(x => employee.IdCard != null && x.IdCard == employee.IdCard);
                if (hasIdCard)
                {
                    ModelState.AddModelError("IdCard", "The IdCard is already in use");
                }
                if (ModelState.IsValid)
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Employees", null);
                }
            }
            catch (Exception)
            {

                //logging
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Skills = this.GetSkills();
            ViewBag.Departments = this.GetDepartment();
            return View(employee);
        }

        #endregion

        [HttpGet]
        public JsonResult IsIdCardAvailable(string IdCard, string OriginIdCard)
        {

            bool hasIdCard = false;
            if (IdCard != OriginIdCard)
            {
                hasIdCard = context.Employees.Any(x => IdCard != null && x.IdCard == IdCard);
            }
            return Json(!hasIdCard, JsonRequestBehavior.AllowGet);

        }

        [NonAction]
        private List<SelectListItem> GetSkills()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListGroup group1 = new SelectListGroup() { Name = "group1" };
            SelectListGroup group2 = new SelectListGroup() { Name = "group1" };

            int[] skills = (int[])Enum.GetValues(typeof(Skill));
            int count = 0;
            foreach (var item in skills)
            {
                SelectListItem selectItem = new SelectListItem()
                {
                    Text = ((Skill)item).ToString(),
                    Value = item.ToString(),
                    Group = count % 2 == 0 ? group1 : group2
                };
                list.Add(selectItem);
                count++;

            }
            return list;
        }

        [NonAction]
        private List<SelectListItem> GetDepartment()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<Department> depatments = context.Departments.ToList();

            foreach (var item in depatments)
            {
                SelectListItem selectItem = new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.DepartmentId.ToString(),

                };
                list.Add(selectItem);
            }
            return list;
        }

        #region Create - UpdateModel
        [HttpGet]
        [ActionName("Create1")]
        public ActionResult Create1_Get()
        {
            ViewBag.Skills = this.GetSkills();
            ViewBag.Departments = this.GetDepartment();
            return View();
        }

        [HttpPost]
        [ActionName("Create1")]
        [ValidateAntiForgeryToken]
        public ActionResult Create1_Post()
        {
            Employee employee = new Employee();
            try
            {
                UpdateModel(employee);
                bool hasIdCard = context.Employees.Any(x => employee.IdCard != null && x.IdCard == employee.IdCard);
                if (hasIdCard)
                {
                    ModelState.AddModelError("IdCard", "The IdCard is already in use");
                }
                if (ModelState.IsValid)
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Employees", null);
                }
            }
            catch (Exception)
            {

                //logging
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Skills = this.GetSkills();
            ViewBag.Departments = this.GetDepartment();
            return View(employee);
        }
        #endregion

        #region TryUpdateModel
        [HttpGet]
        [ActionName("Create2")]
        public ActionResult Create2_Get()
        {
            ViewBag.Skills = this.GetSkills();
            ViewBag.Departments = this.GetDepartment();
            return View();
        }

        [HttpPost]
        [ActionName("Create2")]
        [ValidateAntiForgeryToken]
        public ActionResult Create2_Post()
        {
            Employee employee = new Employee();
            try
            {
                TryUpdateModel(employee);
                bool hasIdCard = context.Employees.Any(x => employee.IdCard != null && x.IdCard == employee.IdCard);
                if (hasIdCard)
                {
                    ModelState.AddModelError("IdCard", "The IdCard is already in use");
                }
                if (ModelState.IsValid)
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Employees", null);
                }
            }
            catch (Exception)
            {

                //logging
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Skills = this.GetSkills();
            ViewBag.Departments = this.GetDepartment();
            return View(employee);
        }
        #endregion

        #region Create - Parameter
        [HttpGet]
        [ActionName("Create3")]
        public ActionResult Create3_Get()
        {
            ViewBag.Skills = this.GetSkills();
            ViewBag.Departments = this.GetDepartment();
            return View();
        }

        [HttpPost]
        [ActionName("Create3")]
        [ValidateAntiForgeryToken]
        public ActionResult Create3_Post(string FirstName, decimal? Salary)
        {
            Employee employee = new Employee();
            try
            {
                if (!string.IsNullOrEmpty(FirstName))
                {
                    ModelState.AddModelError("FirstName", "The FirstName field is required.");
                }
                else if (FirstName.Length <= 2)
                {
                    ModelState.AddModelError("FirstName", "The FirstName length must be greater than 2.");
                }
                else
                {
                    employee.FirstName = FirstName;
                }

                if (!Salary.HasValue)
                {
                    ModelState.AddModelError("Salary", "The Salary field is required.");
                }
                else if (Salary < 1000 || Salary > 10000)
                {
                    ModelState.AddModelError("Salary", "The Salary must be between 1000 and 10000");
                }
                else
                {
                    employee.Salary = Convert.ToDecimal(Salary);
                }

                bool hasIdCard = context.Employees.Any(x => employee.IdCard != null && x.IdCard == employee.IdCard);
                if (hasIdCard)
                {
                    ModelState.AddModelError("IdCard", "The IdCard is already in use");
                }
                if (ModelState.IsValid)
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Employees", null);
                }
            }
            catch (Exception)
            {

                //logging
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Skills = this.GetSkills();
            ViewBag.Departments = this.GetDepartment();
            return View(employee);
        }
        #endregion

        #region Form Collection
        [HttpGet]
        [ActionName("Create4")]
        public ActionResult Create4_Get()
        {
            ViewBag.Skills = this.GetSkills();
            ViewBag.Departments = this.GetDepartment();
            return View();
        }

        [HttpPost]
        [ActionName("Create4")]
        [ValidateAntiForgeryToken]
        public ActionResult Create4_Post(FormCollection formCollection, IEnumerable<int> EditSkills)
        {
            Employee employee = new Employee();
            try
            {
                string FirstName = formCollection["FirstName"];
                string SalaryTemp = formCollection["Salary"];
                if (!string.IsNullOrEmpty(FirstName))
                {
                    ModelState.AddModelError("FirstName", "The FirstName field is required.");
                }
                else if (FirstName.Length <= 2)
                {
                    ModelState.AddModelError("FirstName", "The FirstName length must be greater than 2.");
                }
                else
                {
                    employee.FirstName = FirstName;
                }
                decimal Salary = 0;
                if (!string.IsNullOrEmpty(SalaryTemp))
                {
                    ModelState.AddModelError("Salary", "The Salary field is required.");
                }
                else if (decimal.TryParse(SalaryTemp, out Salary))
                {
                    ModelState.AddModelError("Salary", "The Salary must be number");
                }
                else if (Salary < 1000 || Salary > 10000)
                {
                    ModelState.AddModelError("Salary", "The Salary must be between 1000 and 10000");
                }
                else
                {
                    employee.Salary = Convert.ToDecimal(Salary);
                }

                bool hasIdCard = context.Employees.Any(x => employee.IdCard != null && x.IdCard == employee.IdCard);
                if (hasIdCard)
                {
                    ModelState.AddModelError("IdCard", "The IdCard is already in use");
                }
                if (ModelState.IsValid)
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Employees", null);
                }
            }
            catch (Exception)
            {

                //logging
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Skills = this.GetSkills();
            ViewBag.Departments = this.GetDepartment();
            return View(employee);
        }
        #endregion

        #region HttpContext
        [HttpGet]
        [ActionName("Create5")]
        public ActionResult Create5_Get()
        {
            ViewBag.Skills = this.GetSkills();
            ViewBag.Departments = this.GetDepartment();
            return View();
        }

        [HttpPost]
        [ActionName("Create5")]
        [ValidateAntiForgeryToken]
        public ActionResult Create5_Post(IEnumerable<int> EditSkills)
        {
            Employee employee = new Employee();
            try
            {
                var forms = this.HttpContext.Request.Form;
                string FirstName = forms["FirstName"];
                string SalaryTemp = forms["Salary"];
                if (!string.IsNullOrEmpty(FirstName))
                {
                    ModelState.AddModelError("FirstName", "The FirstName field is required.");
                }
                else if (FirstName.Length <= 2)
                {
                    ModelState.AddModelError("FirstName", "The FirstName length must be greater than 2.");
                }
                else
                {
                    employee.FirstName = FirstName;
                }
                decimal Salary = 0;
                if (!string.IsNullOrEmpty(SalaryTemp))
                {
                    ModelState.AddModelError("Salary", "The Salary field is required.");
                }
                else if (decimal.TryParse(SalaryTemp, out Salary))
                {
                    ModelState.AddModelError("Salary", "The Salary must be number");
                }
                else if (Salary < 1000 || Salary > 10000)
                {
                    ModelState.AddModelError("Salary", "The Salary must be between 1000 and 10000");
                }
                else
                {
                    employee.Salary = Convert.ToDecimal(Salary);
                }

                bool hasIdCard = context.Employees.Any(x => employee.IdCard != null && x.IdCard == employee.IdCard);
                if (hasIdCard)
                {
                    ModelState.AddModelError("IdCard", "The IdCard is already in use");
                }
                if (ModelState.IsValid)
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Employees", null);
                }
            }
            catch (Exception)
            {

                //logging
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Skills = this.GetSkills();
            ViewBag.Departments = this.GetDepartment();
            return View(employee);
        }
        #endregion

        #region Edit
        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit_Get(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = context.Employees.Find(id);
            if (employee == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            employee.ConfirmExpiryDate = employee.ExpiryDate;
            ViewBag.Skills = this.GetSkills();
            ViewBag.Departments = this.GetDepartment();
            return View(employee);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Post(int? id, string OriginIdCard)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = context.Employees.Find(id);
            if (employee == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            TryUpdateModel(employee);
            if (employee.IdCard != OriginIdCard)
            {
                bool hasIdCard = context.Employees.Any(x => employee.IdCard != null && x.IdCard == employee.IdCard);
                if (hasIdCard)
                {
                    ModelState.AddModelError("IdCard", "The IdCard is already in use");
                }
            }
            try
            {
                if (ModelState.IsValid)
                {
                    context.SaveChanges();
                    return RedirectToAction("Index", "Employees", null);
                }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            employee.ConfirmExpiryDate = employee.ExpiryDate;
            ViewBag.Skills = this.GetSkills();
            ViewBag.Departments = this.GetDepartment();
            return View(employee);
        }
        #endregion

        #region Parameters, FormCollection, HttpContext - TryValidateModel
        [HttpGet]
        [ActionName("Create6")]
        public ActionResult Create6_Get()
        {
            ViewBag.Skills = this.GetSkills();
            ViewBag.Departments = this.GetDepartment();
            return View();
        }

        [HttpPost]
        [ActionName("Create6")]
        [ValidateAntiForgeryToken]
        public ActionResult Create6_Post(IEnumerable<int> EditSkills)
        {
            Employee employee = new Employee();
            try
            {
                var forms = this.HttpContext.Request.Form;
                employee.FirstName = forms["FirstName"];
                decimal salaryTemp = 0;
                if (decimal.TryParse(forms["Salary"], out salaryTemp))
                {
                    employee.Salary = salaryTemp;
                }

                bool hasIdCard = context.Employees.Any(x => employee.IdCard != null && x.IdCard == employee.IdCard);
                if (hasIdCard)
                {
                    ModelState.AddModelError("IdCard", "The IdCard is already in use");
                }
                if (TryValidateModel(employee))
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Employees", null);
                }
            }
            catch (Exception)
            {

                //logging
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Skills = this.GetSkills();
            ViewBag.Departments = this.GetDepartment();
            return View(employee);
        }

        #endregion

        #region Delete
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult Delete_Get(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.SeachValue = HttpContext.Request.Form["searchValue"];
            ViewBag.SortField = HttpContext.Request.Form["sortField"];
            ViewBag.PageNumber = HttpContext.Request.Form["pageNumber"];

            return View(id);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Post(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = context.Employees.Find(id);
            if (employee == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            try
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
                return RedirectToAction("Index", "Employees",
                    new
                    {
                        searchValue = HttpContext.Request.QueryString["searchValue"],
                        sortField = HttpContext.Request.QueryString["sortField"],
                        pageNumber = HttpContext.Request.QueryString["pageNumber"]
                    });
            }
            catch (Exception)
            {

                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMany(IEnumerable<int> deletedList)
        {
            bool deleteSuccess = false;
            try
            {
                if (deletedList == null)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                else
                {
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        IQueryable<Employee> deletedEmployee = context.Employees.Where(
                            x => deletedList.Contains(x.EmployeeId));
                        context.Employees.RemoveRange(deletedEmployee);
                        context.SaveChanges();
                        scope.Complete();
                        deleteSuccess = true;
                    }
                }
            }
            catch (Exception)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            }
            return Json(deleteSuccess);
        }
        #endregion


    }
}