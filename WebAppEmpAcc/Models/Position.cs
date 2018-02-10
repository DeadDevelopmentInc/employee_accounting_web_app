using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEmpAcc.Models
{
    public class Position
    {
        //Unique position key
        [Key]
        public string Id { get; set; }

        //Name of position
        [Required]
        public string Name { get; set; }

        //Id of deparmtent 
        [Required]
        public string PlaceId { get; set; }

        //If this is the place of the head of the department, then remember it id
        public string HeadId { get; set; }

        //Value of head taken
        public bool IsHead { get; set; }

        //Access level of position
        public int AccessLvl { get; set; }
    }
}
