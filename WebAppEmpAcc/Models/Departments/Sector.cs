using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEmpAcc.Models.Departments
{
    public class Sector
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsHead { get; set; }

        public string HeadId { get; set; }

        [Required]
        public string BrnchId { get; set; }
        
        public string BrnchName { get; set; }
        
        public string DprtmntId { get; set; }
        
        public string DprtmntName { get; set; }
    }
}
