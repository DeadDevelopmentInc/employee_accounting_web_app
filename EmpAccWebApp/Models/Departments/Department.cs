using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAccWebApp.Models.Departments
{
    public class Department
    {
        //Unique department key
        [Key]
        public string Id { get; set; }
        //Department name
        [Required]
        [Display(Name = "Department name")]
        public string Name { get; set; }
        //If there is a boss, this is head id
        public string HeadId { get; set; }
    }
}
