using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEmpAcc.Models.DepartmentModels
{
    [Serializable]
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsHead { get; set; }
        public List<Branch> Branchs { get; set; }

        public Department()
        {

        }
    }
}
