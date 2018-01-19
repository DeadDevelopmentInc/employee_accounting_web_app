using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEmpAcc.Models.DepartmentModels
{
    [Serializable]
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsHead { get; set; }
        public int DepartmentId { get; set; }
        public List<Sector> Sectors { get; set; }

        public Branch()
        {

        }
    }
}
