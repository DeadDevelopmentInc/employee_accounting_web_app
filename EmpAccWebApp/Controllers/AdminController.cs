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
        //Access levels for users
        private const byte ADMINTACCSSLVL = 5;
        private const byte DEPARTMENTACCSSLVL = 4;
        private const byte BRANCHACCSSLVL = 3;
        private const byte SECTORACCSSLVL = 2;
        private const byte EMPLOYEEACCSSLVL = 1;
        private const byte STUDENTACCSSLVL = 0;

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
        public ActionResult Employees() => PartialView(UserManager.Users.ToList());

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
                var dep = ApplicationDbContext.Departments.Find(item.DprtmntId);
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
        
        //GET: Creat Employee
        public ActionResult CreateEmployee() => PartialView();

        //GET: Creat Department
        public ActionResult CreateDepartment() => PartialView();

        //GET: Creat Branch
        public ActionResult CreateBranch() => PartialView();

        //GET: Creat Sector
        public ActionResult CreateSector() => PartialView();

        /// <summary>
        /// POST: Admin/Create Employee
        /// </summary>
        /// <param name="model"> Model of new employee</param>
        /// <returns>View</returns>
        [HttpPost]
        public async Task<ActionResult> CreateEmployee(RegisterNewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Create new user model
                ApplicationUser user = new ApplicationUser
                {
                    Surname = model.Surname,
                    FirstName = model.FirtsName,
                    SecondName = model.SecondName,
                    FullName = model.Surname + " " + model.FirtsName + " " + model.SecondName,
                    Email = model.Email,
                    UserName = model.Email,
                };
                //Add user with current password in db
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Employees");
                }
            }
            return PartialView(model);
        }

        ///// <summary>
        ///// GET: Admin/Edit Employee
        ///// </summary>
        ///// <param name="id">Id of employee</param>
        ///// <returns>View</returns>
        //public async Task<ActionResult> EditEmployee(string id)
        //{
        //    ApplicationUser user = await UserManager.FindByIdAsync(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    IndexViewModel model = new IndexViewModel
        //    {
        //        Id = user.Id,
        //        Surname = user.Surname,
        //        FirstName = user.FirstName,
        //        SecondName = user.SecondName,
        //    };
        //    return View(model);
        //}

        ///// <summary>
        ///// POST: Admin/Edit Employee
        ///// </summary>
        ///// <param name="model">Model of current emploee</param>
        ///// <returns>View</returns>
        //[HttpPost]
        //public async Task<ActionResult> EditEmployee(IndexViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ApplicationUser user = await UserManager.FindByIdAsync(model.Id);
        //        if (user != null)
        //        {
        //            if (user.Email != model.Email)
        //            {
        //                user.EmailConfirmed = false;
        //                user.Email = model.Email;
        //            }
        //            var result = await UserManager.UpdateAsync(user);
        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("Employees");
        //            }
        //        }
        //    }
        //    return View(model);
        //}

        /// <summary>
        /// GET: Admin/Delete Employee
        /// </summary>
        /// <param name="id">Id of selected employee for deleting</param>
        /// <returns>Redirect to Employee Action </returns>
        public async Task<ActionResult> DeleteEmployee(string id)
        {
            //Find user by id
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                //Delete links with current user
                ApplicationDbContext.DeleteUserFromCurrentPosition(user);
                //Remove user and update db
                await UserManager.DeleteAsync(user);
                await ApplicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Employees");
        }

        /// <summary>
        /// POST: Admin/Create Department
        /// </summary>
        /// <param name="model">Model of new department</param>
        /// <returns>View</returns>
        [HttpPost]
        public async Task<ActionResult> CreateDepartment(DepartmentViewModel model)
        {
            string headId = null;
            //Check model
            if (ModelState.IsValid)
            {
                if(model.HeadFullName != null)
                {
                    //Find new head for department
                    var head = await ApplicationDbContext.FindUserByFullNameAsync(model.HeadFullName);
                    //Delete old position
                    ApplicationDbContext.DeleteUserFromCurrentPosition(head);
                    headId = head.Id;
                    head.AccessLvl = DEPARTMENTACCSSLVL;
                }
                Department dep = new Department
                {
                    Id = Convert.ToString(Guid.NewGuid()),
                    Name = model.Name,
                    HeadId = headId
                };
                //Add department and save changes
                ApplicationDbContext.Departments.Add(dep);
                await ApplicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Departments");
        }

        /// <summary>
        /// POST: Admin/Create Branch
        /// </summary>
        /// <param name="model">Model of new branch</param>
        /// <returns>View</returns>
        [HttpPost]
        public async Task<ActionResult> CreateBranch(BranchViewModel model)
        {
            if (ModelState.IsValid)
            {
                string headId = null;
                if (model.HeadFullName != null)
                {
                    var head = await ApplicationDbContext.FindUserByFullNameAsync(model.HeadFullName);
                    ApplicationDbContext.DeleteUserFromCurrentPosition(head);
                    headId = head.Id;
                    head.AccessLvl = BRANCHACCSSLVL;
                }
                //Find department id for current branch
                var dep = await ApplicationDbContext.FindIdOfDepartmentByNameAsync(model.DepartmentName);
                Branch brc = new Branch
                {
                    Id = Convert.ToString(Guid.NewGuid()),
                    Name = model.Name,
                    HeadId = headId,
                    DprtmntId = dep
                };
                ApplicationDbContext.Branchs.Add(brc);
                await ApplicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Branchs");
        }

        /// <summary>
        /// POST: Admin/Create Sector
        /// </summary>
        /// <param name="model">Model of new sector</param>
        /// <returns>View</returns>
        [HttpPost]
        public async Task<ActionResult> CreateSector(SectorViewModel model)
        {
            if (ModelState.IsValid)
            {
                string headId = null;
                if (model.HeadFullName != null)
                {
                    var head = await ApplicationDbContext.FindUserByFullNameAsync(model.HeadFullName);
                    ApplicationDbContext.DeleteUserFromCurrentPosition(head);
                    headId = head.Id;
                    head.AccessLvl = SECTORACCSSLVL;
                }
                //Find branch id for current sector
                var brc = await ApplicationDbContext.FindIdOfBranchByNameAsync(model.BranchName);
                Sector sec = new Sector
                {
                    Id = Convert.ToString(Guid.NewGuid()),
                    Name = model.Name,
                    HeadId = headId,
                    BrnchId = brc
                };
                ApplicationDbContext.Sectors.Add(sec);
                await ApplicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Sectors");
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
            //Create department view model
            DepartmentViewModel department = new DepartmentViewModel
            {
                Id = dep.Id,
                Name = dep.Name,
            };
            //Add head full name
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
            Branch brc = await ApplicationDbContext.Branchs.FindAsync(id);
            Department dep = await ApplicationDbContext.Departments.FindAsync(brc.DprtmntId);
            if (brc == null) { return HttpNotFound(); }
            BranchViewModel department = new BranchViewModel
            {
                Id = brc.Id,
                Name = brc.Name,
                DepartmentName = dep.Name
            };
            if (brc.HeadId != null)
            {
                ApplicationUser head = await UserManager.FindByIdAsync(brc.HeadId);
                if (head != null) { department.HeadFullName = head.FullName; }
            }

            return PartialView(department);
        }

        /// <summary>
        /// GET: Admin/Edit Sector
        /// </summary>
        /// <param name="id">Id of sector</param>
        /// <returns>View</returns>
        public async Task<ActionResult> EditSector(string id)
        {
            Sector sec = await ApplicationDbContext.Sectors.FindAsync(id);
            Branch brc = await ApplicationDbContext.Branchs.FindAsync(sec.BrnchId);
            if (sec == null) { return HttpNotFound(); }
            SectorViewModel department = new SectorViewModel
            {
                Id = sec.Id,
                Name = sec.Name,
                BranchName = brc.Name                
            };
            if (sec.HeadId != null)
            {
                ApplicationUser head = await UserManager.FindByIdAsync(sec.HeadId);
                if (head != null) { department.HeadFullName = head.FullName; }
            }

            return PartialView(department);
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
            //Change name of department
            if(department.Name != dep.Name) { department.Name = dep.Name; }
            //Check new new head full name
            if(dep.HeadFullName == null)
            {
                //Delete old user
                if(department.HeadId != null)
                {
                    var user = await UserManager.FindByIdAsync(department.HeadId);
                    ApplicationDbContext.DeleteUserFromCurrentPosition(user);
                    department.HeadId = null;
                }
            }
            else
            {
                //Find new head
                ApplicationUser head = await ApplicationDbContext.FindUserByFullNameAsync(dep.HeadFullName);
                ApplicationDbContext.DeleteUserFromCurrentPosition(head);
                //Compare new and old heads id
                if (head.Id != department.HeadId)
                {
                    //Delete old head
                    if(department.HeadId != null)
                    {
                        var user = ApplicationDbContext.Users.Find(department.HeadId);
                        ApplicationDbContext.DeleteUserFromCurrentPosition(user);
                    }
                    //Update new head
                    department.HeadId = head.Id;
                    head.AccessLvl = DEPARTMENTACCSSLVL;
                }
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
            //Find department
            Department dep = await ApplicationDbContext.Departments.FindAsync(id);
            if (dep != null)
            {
                //Clear link with old head
                if(dep.HeadId != null)
                {
                    var head = await UserManager.FindByIdAsync(dep.HeadId);
                    ApplicationDbContext.DeleteUserFromCurrentPosition(head);
                }
                //Delete branchs and sectors in this department
                ApplicationDbContext.DeleteBranchInCurrentDepartment(dep.Id);
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
            //Find branch
            Branch brc = await ApplicationDbContext.Branchs.FindAsync(id);
            if (brc != null)
            {
                //Clear link with old head
                if (brc.HeadId != null)
                {
                    var head = await UserManager.FindByIdAsync(brc.HeadId);
                    ApplicationDbContext.DeleteUserFromCurrentPosition(head);
                }
                //Delete sectors in currenct branch
                ApplicationDbContext.DeleteSectorInCurrentBranch(brc.Id);
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
            //Find sector
            Sector sec = await ApplicationDbContext.Sectors.FindAsync(id);
            if (sec != null)
            {
                //Clear link with old head
                if (sec.HeadId != null)
                {
                    var head = await UserManager.FindByIdAsync(sec.HeadId);
                    ApplicationDbContext.DeleteUserFromCurrentPosition(head);
                }
                ApplicationDbContext.Sectors.Remove(sec);
                await ApplicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Sectors");
        }

        #region Helpers

        public ActionResult AutocompleteSearchUsers(string term)
        {
            //Find term in users full name and cteate model 
            var models = ApplicationDbContext.Users.Where(a => a.FullName.Contains(term))
                            .Select(a => new { value = a.FullName})
                            .Distinct();

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutocompleteSearchDepartments(string term)
        {
            //Find term in departments name and create model 
            var models = ApplicationDbContext.Departments.Where(a => a.Name.Contains(term))
                            .Select(a => new { value = a.Name })
                            .Distinct();

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutocompleteSearchBranchs(string term)
        {
            //Find term in branchs name and cteate model 
            var models = ApplicationDbContext.Branchs.Where(a => a.Name.Contains(term))
                            .Select(a => new { value = a.Name })
                            .Distinct();

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}