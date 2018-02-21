using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EmpAccWebApp.Models.Departments;

namespace EmpAccWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FullName { get; set; }
        public int AccessLvl { get; set; }
        public string WorkPhoneNumber { get; set; }
        public string Place { get; set; }
        public string Adress { get; set; }
        public string ProfilePhoto { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<SkillsModel> ApplicationUserSkills { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        /// <summary>
        /// Find user in database by full name
        /// </summary>
        /// <param name="fullName">Full name of user</param>
        /// <returns>Found user or null</returns>
        public async Task<ApplicationUser> FindUserByFullNameAsync(string fullName)
        {
            return await Task.Run(() =>
            {
                foreach(var item in Users)
                {
                    if(item.FullName == fullName) { return item; }
                }
                return null;
            });
        }

        public async Task<string> FindIdOfDepartmentByNameAsync(string nameDep)
        {
            return await Task.Run(() =>
            {
                foreach(var item in Departments)
                {
                    if (item.Name == nameDep) { return item.Id; }
                }
                return null;
            });
        }

        public async Task<string> FindIdOfBranchByNameAsync(string nameBrc)
        {
            return await Task.Run(() =>
            {
                foreach (var item in Branchs)
                {
                    if (item.Name == nameBrc) { return item.Id; }
                }
                return null;
            });
        }

        /// <summary>
        /// Find department by head id
        /// </summary>
        /// <param name="userId">Head id</param>
        /// <returns>Found department or null</returns>
        public Department GetDepartment(string userId)
        {
            foreach (var item in Departments)
            {
                if (item.HeadId == userId) { return item; }
            }
            return null;
        }

        /// <summary>
        /// Find branch by head id
        /// </summary>
        /// <param name="userId">Head id</param>
        /// <returns>Found branch or null</returns>
        public Branch GetBranch(string userId)
        {
            foreach (var item in Branchs)
            {
                if (item.HeadId == userId) { return item; }
            }
            return null;
        }

        /// <summary>
        /// Find sector by head id
        /// </summary>
        /// <param name="userId">Head id</param>
        /// <returns>Found sector or null</returns>
        public Sector GetSector(string userId)
        {
            foreach (var item in Sectors)
            {
                if (item.HeadId == userId) { return item; }
            }
            return null;
        }

        public void DeleteSectorInCurrentBranch(string branchId)
        {
            foreach(var item in Sectors)
            {
                if(branchId == item.BrnchId)
                {
                    if(item.HeadId != null)
                    {
                        var user = Users.Find(item.HeadId);
                        DeleteUserFromCurrentPosition(user);
                    }
                    Sectors.Remove(item);
                }
            }
        }

        public void DeleteBranchInCurrentDepartment(string departmentId)
        {
            foreach(var item in Branchs)
            {
                if(departmentId == item.DprtmntId)
                {
                    if (item.HeadId != null)
                    {
                        var user = Users.Find(item.HeadId);
                        DeleteUserFromCurrentPosition(user);
                    }
                    DeleteSectorInCurrentBranch(item.Id);
                    Branchs.Remove(item);
                }
            }
        }

        public void DeleteUserFromCurrentPosition(ApplicationUser user)
        {
            int EMPLOYEEACCSSLVL = 1;
            if (user.AccessLvl > EMPLOYEEACCSSLVL)
            {
                switch (user.AccessLvl)
                {
                    case 2:
                        {
                            var sector = GetSector(user.Id);
                            sector.HeadId = null;
                        }
                        break;
                    case 3:
                        {
                            var branch = GetBranch(user.Id);
                            branch.HeadId = null;
                        }
                        break;
                    case 4:
                        {
                            var department = GetDepartment(user.Id);
                            department.HeadId = null;
                        }
                        break;
                }
                user.AccessLvl = EMPLOYEEACCSSLVL;
            }
        }
    }
}