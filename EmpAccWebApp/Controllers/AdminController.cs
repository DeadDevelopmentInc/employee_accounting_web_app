using EmpAccWebApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EmpAccWebApp.Models.Departments;
using Microsoft.AspNet.Identity.Owin;
using static EmpAccWebApp.Models.AdminViewModels;

namespace EmpAccWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationUserManager _userManager;

        public AdminController() { }

        public AdminController(ApplicationDbContext context,
            ApplicationUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get
            {
                return _context ?? HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            }
            private set
            {
                _context = value;
            }
        }

        //GET: Admin
        public ActionResult Index() => PartialView();

        //GET: Employees
        public ActionResult Employees() => PartialView(_userManager.Users.ToList());

        //GET: Departments
        public async Task<ActionResult> Departments()
        {
            string headfullname = null;
            List<DepartmentViewModel> departments = new List<DepartmentViewModel>();
            foreach(var item in ApplicationDbContext.Departments)
            {

                if (item.HeadId != null)
                {
                    var user = await UserManager.FindByIdAsync(item.HeadId);
                    if(user != null) { headfullname = user.FullName; } 
                }
                departments.Add(new DepartmentViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    HeadFullName = headfullname,
                });
                headfullname = null;
            }
            return PartialView(departments);
        }
        

        //GET:  Branchs
        public async Task<ActionResult> Branchs()
        {
            string headfullname = null;
            List<BranchViewModel> branchs = new List<BranchViewModel>();
            foreach (var item in ApplicationDbContext.Branchs)
            {
                if (item.HeadId != null)
                {
                    var user = await UserManager.FindByIdAsync(item.HeadId);
                    if (user != null) { headfullname = user.FullName; }
                }
                var dep = await ApplicationDbContext.Departments.FindAsync(item.DprtmntId);
                branchs.Add(new BranchViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    HeadFullName = headfullname,
                    DepartmentName = dep.Name,
                });
                headfullname = null;
            }
            return PartialView(branchs);
        }

        //GET: Sectors
        public async Task<ActionResult> Sectors()
        {
            string headfullname = null;
            List<SectorViewModel> sectors = new List<SectorViewModel>();
            foreach (var item in ApplicationDbContext.Sectors)
            {
                if (item.HeadId != null)
                {
                    var user = await UserManager.FindByIdAsync(item.HeadId);
                    if (user != null) { headfullname = user.FullName; }
                }
                var dep = await ApplicationDbContext.Branchs.FindAsync(item.BrnchId);
                sectors.Add(new SectorViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    HeadFullName = headfullname,
                    BranchName = dep.Name,
                });
                headfullname = null;
            }
            return PartialView(sectors);
        }

        //GET: Projects
        public ActionResult Projects() => View();

        //GET: Creat Employee
        public ActionResult CreateEmployee() => View();

        //GET: Creat Department
        public ActionResult CreateDepartment() => View();

        //GET: Creat Branch
        public ActionResult CreateBranch() => View();

        //GET: Creat Sector
        public ActionResult CreateSector() => View();

        /// <summary>
        /// POST: Admin/Create Employee
        /// </summary>
        /// <param name="model"> Model of new employee</param>
        /// <returns>View</returns>
        [HttpPost]
        public async Task<ActionResult> CreateEmployee(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Surname = model.Surname,
                    FirstName = model.FirtsName,
                    SecondName = model.SecondName,
                    Email = model.Email,
                    UserName = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Employees");
                }
            }
            return View(model);
        }

        /// <summary>
        /// GET: Admin/Edit Employee
        /// </summary>
        /// <param name="id">Id of employee</param>
        /// <returns>View</returns>
        public async Task<ActionResult> EditEmployee(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            IndexViewModel model = new IndexViewModel
            {
                Id = user.Id,
                Surname = user.Surname,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                Email = user.Email
            };
            return View(model);
        }

        /// <summary>
        /// POST: Admin/Edit Employee
        /// </summary>
        /// <param name="model">Model of current emploee</param>
        /// <returns>View</returns>
        [HttpPost]
        public async Task<ActionResult> EditEmployee(IndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    if (user.Email != model.Email)
                    {
                        user.EmailConfirmed = false;
                        user.Email = model.Email;
                    }
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Employees");
                    }
                }
            }
            return View(model);
        }

        /// <summary>
        /// GET: Admin/Delete Employee
        /// </summary>
        /// <param name="id">Id of selected employee for deleting</param>
        /// <returns>Redirect to Employee Action </returns>
        public async Task<ActionResult> DeleteEmployee(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Employees");
        }

        /// <summary>
        /// POST: Admin/Create Department
        /// </summary>
        /// <param name="model">Model of new department</param>
        /// <returns>View</returns>
        [HttpPost]
        public async Task<ActionResult> CreateDepartment(Department model)
        {
            //Check model
            if (ModelState.IsValid)
            {
            }
            return View(model);
        }

        /// <summary>
        /// POST: Admin/Create Branch
        /// </summary>
        /// <param name="model">Model of new branch</param>
        /// <returns>View</returns>
        [HttpPost]
        public async Task<ActionResult> CreateBranch(Branch model)
        {
            if (ModelState.IsValid)
            {
             
            }
            return View(model);
        }

        /// <summary>
        /// POST: Admin/Create Sector
        /// </summary>
        /// <param name="model">Model of new sector</param>
        /// <returns>View</returns>
        [HttpPost]
        public async Task<ActionResult> CreateSector(Sector model)
        {
            if (ModelState.IsValid)
            {
                
            }
            return View(model);
        }

        /// <summary>
        /// GET: Admin/Edit Department
        /// </summary>
        /// <param name="id">Id of department</param>
        /// <returns>View</returns>
        public async Task<ActionResult> EditDepartment(string id)
        {
            Department dep = await ApplicationDbContext.Departments.FindAsync(id);
            if (dep == null)
            {
                return HttpNotFound();
            }
            DepartmentViewModel department = new DepartmentViewModel
            {
                Id = dep.Id,
                Name = dep.Name,
            };
            if (dep.HeadId != null)
            {
                ApplicationUser head = await UserManager.FindByIdAsync(dep.HeadId);
                if(head != null) { department.HeadFullName = head.FullName; }
            }

            return PartialView(department);
        }

        /// <summary>
        /// GET: Admin/Edit Branch
        /// </summary>
        /// <param name="id">Id of branch</param>
        /// <returns>View</returns>
        public async Task<ActionResult> EditBranch(string id)
        {
            Branch brh = await ApplicationDbContext.Branchs.FindAsync(id);

            if (brh == null)
            {
                return HttpNotFound();
            }
            return View(brh);
        }

        /// <summary>
        /// GET: Admin/Edit Sector
        /// </summary>
        /// <param name="id">Id of sector</param>
        /// <returns>View</returns>
        public async Task<ActionResult> EditSector(string id)
        {
            Sector sec = await ApplicationDbContext.Sectors.FindAsync(id);

            if (sec == null)
            {
                return HttpNotFound();
            }
            return View(sec);
        }

        /// <summary>
        /// POST: Admin/Edit Sector
        /// </summary>
        /// <param name="sec">Model of current sector</param>
        /// <returns>Redirect to Action Sectors</returns>
        [HttpPost]
        public async Task<ActionResult> EditSector(Sector sec)
        {           
            return RedirectToAction("Sectors");
        }

        /// <summary>
        /// POST: Admin/Edit Branch
        /// </summary>
        /// <param name="sec">Model of current branch</param>
        /// <returns>Redirect to Action Branchs</returns>
        [HttpPost]
        public async Task<ActionResult> EditBranch(Branch brc)
        {
            return RedirectToAction("Branchs");
        }

        /// <summary>
        /// POST: Admin/Edit Department
        /// </summary>
        /// <param name="sec">Model of current department</param>
        /// <returns>Redirect to Action Departments</returns>
        [HttpPost]
        public async Task<ActionResult> EditDepartment(DepartmentViewModel dep)
        {           
            if(!ModelState.IsValid) { return View(dep); }

            Department department = await ApplicationDbContext.Departments.FindAsync(dep.Id);

            if(department.Name != dep.Name) { department.Name = dep.Name; }
            if(dep.HeadFullName == null)
            {
                if(department.HeadId != null) { department.HeadId = null; }
            }
            else
            {
                ApplicationUser head = await ApplicationDbContext.FindUserByFullNameAsync(dep.HeadFullName);
                if (head.FullName != dep.HeadFullName) { department.HeadId = head.Id; }
            }
            await ApplicationDbContext.SaveChangesAsync();
            return RedirectToAction("Departments");
        }

        /// <summary>
        /// GET: Admin/Delete Department
        /// </summary>
        /// <param name="id">Id of removable department</param>
        /// <returns>Redirect to Action Departments</returns>
        public async Task<ActionResult> DeleteDepartment(string id)
        {
            Department dep = await ApplicationDbContext.Departments.FindAsync(id);
            if (dep != null)
            {
                ApplicationDbContext.Departments.Remove(dep);
                await ApplicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Departments");
        }

        /// <summary>
        /// GET: Admin/Delete Branch
        /// </summary>
        /// <param name="id">Id of removable branch</param>
        /// <returns>Redirect to Action Branchs</returns>
        public async Task<ActionResult> DeleteBranch(string id)
        {
            Branch brc = await ApplicationDbContext.Branchs.FindAsync(id);
            if (brc != null)
            {
                ApplicationDbContext.Branchs.Remove(brc);
                await ApplicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Branchs");
        }

        /// <summary>
        /// GET: Admin/Delete Sector
        /// </summary>
        /// <param name="id">Id of removable sector</param>
        /// <returns>Redirect to Action Sectors</returns>
        public async Task<ActionResult> DeleteSector(string id)
        {
            Sector sec = await ApplicationDbContext.Sectors.FindAsync(id);
            if (sec != null)
            {
                ApplicationDbContext.Sectors.Remove(sec);
                await ApplicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Sectors");
        }

        #region Helpers

        public ActionResult AutocompleteSearch(string term)
        {
            var models = ApplicationDbContext.Users.Where(a => a.FullName.Contains(term))
                            .Select(a => new { value = a.FullName})
                            .Distinct();

            return Json(models);
        }
        #endregion
    }
}