using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEmpAcc.Models
{
    public class ApplicationDepartment
    {
        [Key]
        public string Id { get; set; }
        public string Departments { get; set; }
        public int Lvl { get; }
        public bool IsHead { get; set; }
    }
}
