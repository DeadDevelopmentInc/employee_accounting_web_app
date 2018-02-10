using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppEmpAcc.Data;
using WebAppEmpAcc.Models;
using WebAppEmpAcc.Models.AccountViewModels;
using WebAppEmpAcc.Models.Departments;
using WebAppEmpAcc.Models.ManageViewModels;

namespace WebAppEmpAcc.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //GET: Admin
        public IActionResult Index() => View();

        //GET: Employees
        public IActionResult Employees() => View(_userManager.Users.ToList());

        //GET: Departments
        public IActionResult Departments() => View(_context.Departments.ToList());

        //GET:  Branchs
        public IActionResult Branchs() => View(_context.Branchs.ToList());

        //GET: Sectors
        public IActionResult Sectors() => View(_context.Sectors.ToList());

        //GET: Projects
        public IActionResult Projects() => View();

        //GET: Creat Employee
        public IActionResult CreateEmployee() => View();

        //GET: Creat Department
        public IActionResult CreateDepartment() => View();

        //GET: Creat Branch
        public IActionResult CreateBranch() => View();

        //GET: Creat Sector
        public IActionResult CreateSector() => View();

        /// <summary>
        /// POST: Admin/Create Employee
        /// </summary>
        /// <param name="model"> Model of new employee</param>
        /// <returns>View</returns>
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { FrstName = model.FirtsName, ScndName = model.SecondName,
                    Email = model.Email, UserName = model.Email};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Employees");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        /// <summary>
        /// GET: Admin/Edit Employee
        /// </summary>
        /// <param name="id">Id of employee</param>
        /// <returns>View</returns>
        public async Task<IActionResult> EditEmployee(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            IndexViewModel model = new IndexViewModel {
                Id = user.Id,
                FirstName = user.FrstName,
                SecondName = user.ScndName,
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
        public async Task<IActionResult> EditEmployee(IndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.FrstName = model.FirstName;
                    user.ScndName = model.SecondName;
                    user.Email = model.Email;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Employees");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
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
        public async Task<IActionResult> DeleteEmployee(string id)
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
        public async Task<IActionResult> CreateDepartment(Department model)
        {
            //Check model
            if(ModelState.IsValid)
            {
                //Create new department with name
                Department dep = new Department {
                    Name = model.Name,
                    IsHead = false
                };
                //if we now have head? cheack it
                if (model.HeadId != null)
                {
                    //Get user for head of department
                    var head = await _userManager.FindByIdAsync(model.HeadId);
                    head = await DeleteOldPositionOfNewHead(head);
                    if(head != null)
                    {
                        head.Department = model.Name;
                        head.Branch = null;
                        head.Sector = null;
                        head.AccessLvl = 4;
                        await _userManager.UpdateAsync(head);
                        dep.IsHead = true;
                        dep.HeadId = head.Id;
                    }
                }
                await _context.Departments.AddAsync(dep);
                await _context.SaveChangesAsync();
                return RedirectToAction("Departments");
            }
            return View(model);
        }

        /// <summary>
        /// POST: Admin/Create Branch
        /// </summary>
        /// <param name="model">Model of new branch</param>
        /// <returns>View</returns>
        [HttpPost]
        public async Task<IActionResult> CreateBranch(Branch model)
        {
            if (ModelState.IsValid)
            {
                var Dprtmnt = await _context.Departments.FindAsync(model.DprtmntId);
                var Name = Dprtmnt.Name;
                Branch brc = new Branch
                {
                    Name = model.Name,
                    DprtmntName = Name,
                    DprtmntId = model.DprtmntId,
                    IsHead = false
                };
                if (model.HeadId != null)
                {
                    var head = await _userManager.FindByIdAsync(model.HeadId);
                    head = await DeleteOldPositionOfNewHead(head);
                    if (head != null)
                    {
                        if (head.Department == null)
                        {
                            head.Department = model.Name;
                            head.AccessLvl = 4;
                            await _userManager.UpdateAsync(head);
                            brc.IsHead = true;
                            brc.HeadId = head.Id;
                        }
                    }
                }
                await _context.Branchs.AddAsync(brc);
                await _context.SaveChangesAsync();
                return RedirectToAction("Branchs");
            }
            return View(model);
        }

        /// <summary>
        /// POST: Admin/Create Sector
        /// </summary>
        /// <param name="model">Model of new sector</param>
        /// <returns>View</returns>
        [HttpPost]
        public async Task<IActionResult> CreateSector(Sector model)
        {
            if (ModelState.IsValid)
            {
                var brnch = await _context.Branchs.FindAsync(model.BrnchId);
                if(brnch != null)
                {
                    Sector sec = new Sector
                    {
                        Name = model.Name,
                        DprtmntName = brnch.DprtmntName,
                        DprtmntId = brnch.DprtmntId,
                        BrnchName = brnch.Name,
                        BrnchId = model.BrnchId,
                        IsHead = false
                    };
                    if (model.HeadId != null)
                    {
                        var head = await _userManager.FindByIdAsync(model.HeadId);
                        head = await DeleteOldPositionOfNewHead(head);
                        if (head != null)
                        {
                            if (head.Department == null)
                            {
                                head.Department = model.Name;
                                head.AccessLvl = 4;
                                await _userManager.UpdateAsync(head);
                                sec.IsHead = true;
                                sec.HeadId = head.Id;
                            }
                        }
                    }
                    await _context.Sectors.AddAsync(sec);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Sectors");
                }
            }
            return View(model);
        }

        /// <summary>
        /// GET: Admin/Edit Department
        /// </summary>
        /// <param name="id">Id of department</param>
        /// <returns>View</returns>
        public async Task<IActionResult> EditDepartment(string id)
        {
            Department dep = await _context.Departments.FindAsync(id);

            if(dep == null)
            {
                return NotFound();
            }
            return View(dep);
        }

        /// <summary>
        /// GET: Admin/Edit Branch
        /// </summary>
        /// <param name="id">Id of branch</param>
        /// <returns>View</returns>
        public async Task<IActionResult> EditBranch(string id)
        {
            Branch brh = await _context.Branchs.FindAsync(id);

            if (brh == null)
            {
                return NotFound();
            }
            return View(brh);
        }

        /// <summary>
        /// GET: Admin/Edit Sector
        /// </summary>
        /// <param name="id">Id of sector</param>
        /// <returns>View</returns>
        public async Task<IActionResult> EditSector(string id)
        {
            Sector sec = await _context.Sectors.FindAsync(id);

            if (sec == null)
            {
                return NotFound();
            }
            return View(sec);
        }

        /// <summary>
        /// POST: Admin/Edit Sector
        /// </summary>
        /// <param name="sec">Model of current sector</param>
        /// <returns>Redirect to Action Sectors</returns>
        [HttpPost]
        public async Task<IActionResult> EditSector(Sector sec)
        {
            if (ModelState.IsValid)
            {
                Sector sector = await _context.Sectors.FindAsync(sec.Id);

                if (sector != null)
                {
                    sector.Name = sec.Name;
                    //Check if need change branch of sector
                    if (sector.BrnchId != sec.BrnchId)
                    {
                        var branch = await _context.Branchs.FindAsync(sec.BrnchId);
                        if (branch != null)
                        {
                            HelpFunctionForEditDepartments.ChangeBranchOfSector(ref sector, branch);
                        }
                    }
                    //Check if need put head, but place alredy taken
                    if (sector.IsHead)
                    {
                        //Check if head id was changed
                        if(sec.HeadId != sector.HeadId)
                        {
                            var oldhead = await _userManager.FindByIdAsync(sector.HeadId);

                            if (sec.HeadId != null)
                            {
                                var newhead = await _userManager.FindByIdAsync(sec.HeadId);
                                newhead = await DeleteOldPositionOfNewHead(newhead);
                                if (newhead != null)
                                {
                                    HelpFunctionForEditDepartments.ChangeHeadOfSector(ref newhead, ref oldhead, ref sector);

                                    await _userManager.UpdateAsync(oldhead);
                                    await _userManager.UpdateAsync(newhead);
                                }
                            }
                            else
                            {
                                HelpFunctionForEditDepartments.RemoveHeadOfSector(ref oldhead, ref sector);                                
                            }
                        }
                    }
                    else
                    {
                        //Check if need put new head on free place
                        if(sec.HeadId != sector.HeadId)
                        {
                            var newhead = await _userManager.FindByIdAsync(sec.HeadId);
                            newhead = await DeleteOldPositionOfNewHead(newhead);

                            HelpFunctionForEditDepartments.AddHeadOfSector(ref newhead, ref sector);
                            
                            await _userManager.UpdateAsync(newhead);
                        }
                    }


                    //Check
                    _context.Sectors.Update(sector);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Sectors");
        }

        /// <summary>
        /// POST: Admin/Edit Branch
        /// </summary>
        /// <param name="sec">Model of current branch</param>
        /// <returns>Redirect to Action Branchs</returns>
        [HttpPost]
        public async Task<IActionResult> EditBranch(Branch brc)
        {
            if (ModelState.IsValid)
            {
                Branch branch = await _context.Branchs.FindAsync(brc.Id);

                if (branch != null)
                {
                    branch.Name = brc.Name;
                    //Check if need change department of branch
                    if (branch.DprtmntId != brc.DprtmntId)
                    {
                        var department = await _context.Departments.FindAsync(brc.DprtmntId);
                        if (branch != null)
                        {
                            HelpFunctionForEditDepartments.ChangeDepartmentOfBranch(ref branch, department);
                        }
                    }
                    //Check if need put head, but place alredy taken
                    if (branch.IsHead)
                    {
                        //Check if head id was changed
                        if (brc.HeadId != branch.HeadId)
                        {
                            var oldhead = await _userManager.FindByIdAsync(branch.HeadId);

                            if (brc.HeadId != null)
                            {
                                var newhead = await _userManager.FindByIdAsync(brc.HeadId);
                                newhead = await DeleteOldPositionOfNewHead(newhead);

                                if (newhead != null)
                                {
                                    HelpFunctionForEditDepartments.ChangeHeadOfBranch(ref newhead, ref oldhead, ref branch);

                                    await _userManager.UpdateAsync(oldhead);
                                    await _userManager.UpdateAsync(newhead);
                                }
                            }
                            else
                            {
                                HelpFunctionForEditDepartments.RemoveHeadOfBranch(ref oldhead, ref branch);
                            }
                        }
                    }
                    else
                    {
                        //Check if need put new head on free place
                        if (brc.HeadId != branch.HeadId)
                        {
                            var newhead = await _userManager.FindByIdAsync(brc.HeadId);
                            newhead = await DeleteOldPositionOfNewHead(newhead);

                            HelpFunctionForEditDepartments.AddHeadOfBranch(ref newhead, ref branch);

                            await _userManager.UpdateAsync(newhead);
                        }
                    }


                    //Check
                    _context.Branchs.Update(branch);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Branchs");
        }

        /// <summary>
        /// POST: Admin/Edit Department
        /// </summary>
        /// <param name="sec">Model of current department</param>
        /// <returns>Redirect to Action Departments</returns>
        [HttpPost]
        public async Task<IActionResult> EditDepartment(Department dep)
        {
            if (ModelState.IsValid)
            {
                //Get ex department
                Department department = await _context.Departments.FindAsync(dep.Id);

                if (department != null)
                {
                    department.Name = dep.Name;                    
                    //Check if need put head, but place alredy taken
                    if (department.IsHead)
                    {
                        //Check if head id was changed
                        if (dep.HeadId != department.HeadId)
                        {
                            var oldhead = await _userManager.FindByIdAsync(department.HeadId);

                            if (dep.HeadId != null)
                            {
                                var newhead = await _userManager.FindByIdAsync(dep.HeadId);
                                newhead = await DeleteOldPositionOfNewHead(newhead);

                                if (newhead != null)
                                {
                                    HelpFunctionForEditDepartments.ChangeHeadOfDepartment(ref newhead, ref oldhead, ref department);

                                    await _userManager.UpdateAsync(oldhead);
                                    await _userManager.UpdateAsync(newhead);
                                }
                            }
                            else
                            {
                                HelpFunctionForEditDepartments.RemoveHeadOfDepartment(ref oldhead, ref department);
                            }
                        }
                    }
                    else
                    {
                        //Check if need put new head on free place
                        if (dep.HeadId != department.HeadId)
                        {
                            var newhead = await _userManager.FindByIdAsync(dep.HeadId);
                            newhead = await DeleteOldPositionOfNewHead(newhead);

                            HelpFunctionForEditDepartments.AddHeadOfDepartment(ref newhead, ref department);

                            await _userManager.UpdateAsync(newhead);
                        }
                    }


                    //Check
                    _context.Departments.Update(department);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Departments");
        }
        
        /// <summary>
        /// GET: Admin/Delete Department
        /// </summary>
        /// <param name="id">Id of removable department</param>
        /// <returns>Redirect to Action Departments</returns>
        public async Task<IActionResult> DeleteDepartment(string id)
        {
            Department dep = await _context.Departments.FindAsync(id);
            if (dep != null)
            {
                if (dep.IsHead == true)
                {
                    var head = await _userManager.FindByIdAsync(dep.HeadId);
                    HelpFunctionForEditDepartments.RemoveHeadOfDepartment(ref head, ref dep);
                    await _userManager.UpdateAsync(head);
                }
                _context.Departments.Remove(dep);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Departments");
        }

        /// <summary>
        /// GET: Admin/Delete Branch
        /// </summary>
        /// <param name="id">Id of removable branch</param>
        /// <returns>Redirect to Action Branchs</returns>
        public async Task<IActionResult> DeleteBranch(string id)
        {
            Branch brc = await _context.Branchs.FindAsync(id);
            if (brc != null)
            {
                if (brc.IsHead == true)
                {
                    var head = await _userManager.FindByIdAsync(brc.HeadId);
                    HelpFunctionForEditDepartments.RemoveHeadOfBranch(ref head, ref brc);
                    await _userManager.UpdateAsync(head);
                }
                _context.Branchs.Remove(brc);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Branchs");
        }

        /// <summary>
        /// GET: Admin/Delete Sector
        /// </summary>
        /// <param name="id">Id of removable sector</param>
        /// <returns>Redirect to Action Sectors</returns>
        public async Task<IActionResult> DeleteSector(string id)
        {
            Sector sec = await _context.Sectors.FindAsync(id);
            if (sec != null)
            {
                if (sec.IsHead == true)
                {
                    var head = await _userManager.FindByIdAsync(sec.HeadId);
                    HelpFunctionForEditDepartments.RemoveHeadOfSector(ref head, ref sec);
                    await _userManager.UpdateAsync(head);
                }
                _context.Sectors.Remove(sec);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Sectors");
        }

        #region Helpers

        /// <summary>
        /// Check and delete if current user be a head
        /// </summary>
        /// <param name="user">Test User</param>
        /// <returns>Return user</returns>
        private async Task<ApplicationUser> DeleteOldPositionOfNewHead(ApplicationUser user)
        {
            if(user.AccessLvl > 1 & user.Position != null) // 1 level of access for employee
            {
                switch(user.AccessLvl)
                {
                    case 2:
                        {
                            var sector = await _context.Sectors.FindAsync(user.Position);
                            HelpFunctionForEditDepartments.RemoveHeadOfSector(ref user, ref sector);
                            _context.Sectors.Update(sector);
                        }
                        break;
                    case 3:
                        {
                            var branch = await _context.Branchs.FindAsync(user.Position);
                            HelpFunctionForEditDepartments.RemoveHeadOfBranch(ref user, ref branch);
                            _context.Branchs.Update(branch);
                        }
                        break;
                    case 4:
                        {
                            var department = await _context.Departments.FindAsync(user.Position);
                            HelpFunctionForEditDepartments.RemoveHeadOfDepartment(ref user, ref department);
                            _context.Departments.Update(department);
                        }
                        break;
                }
                await _userManager.UpdateAsync(user);
                await _context.SaveChangesAsync();
            }
            return user;
        }

        #endregion
    }
}
