using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAccWebApp.Models.Departments
{
    public class Sector
    {
        //Unique sector key
        [Key]
        public string Id { get; set; }
        //Sector name
        [Required]
        public string Name { get; set; }
        //If there is a boss, this is head id
        public string HeadId { get; set; }
        //Branch id
        [Required]
        public string BrnchId { get; set; }
    }
}
