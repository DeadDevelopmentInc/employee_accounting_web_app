using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEmpAcc.Models
{
    public class Picture
    {
        [Key]
        public string Id { get; set; }
        public string Path { get; set; }
    }
}
