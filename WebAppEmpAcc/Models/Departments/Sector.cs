using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEmpAcc.Models.Departments
{
    public class Sector
    {
        //Unique sector key
        [Key]
        public string Id { get; set; }

        //Sector name
        [Required]
        public string Name { get; set; }

        //Is the post busy
        [Required]
        public bool IsHead { get; set; }

        //If there is a boss, this is head id
        public string HeadId { get; set; }

        //Branch id
        [Required]
        public string BrnchId { get; set; }
        
        //Branch name
        public string BrnchName { get; set; }
        
        //Department id
        public string DprtmntId { get; set; }
        
        //Department name
        public string DprtmntName { get; set; }
    }
}
