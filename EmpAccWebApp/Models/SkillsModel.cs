using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpAccWebApp.Models
{
    public class SkillsModel
    {
        [Key]
        public string Id { get; set; }
        public int C_Sharp { get; set; } = 0;
        public int C_or_Cpp { get; set; } = 0;
        public int Java { get; set; } = 0;
        public int Python { get; set; } = 0;
        public int SQL { get; set; } = 0;
        public int Ruby { get; set; } = 0;
        public int VB { get; set; } = 0;
        public int Perl { get; set; } = 0;
    }
}