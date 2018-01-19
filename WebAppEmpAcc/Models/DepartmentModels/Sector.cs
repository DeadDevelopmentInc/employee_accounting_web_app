using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEmpAcc.Models.DepartmentModels
{
    [Serializable]
    public class Sector
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsHead { get; set; }
        public int DepartmentId { get; set; }
        public int BranchId { get; set; }

        public Sector()
        {

        }
    }
}
