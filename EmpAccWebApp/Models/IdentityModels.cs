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

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

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
    }
}