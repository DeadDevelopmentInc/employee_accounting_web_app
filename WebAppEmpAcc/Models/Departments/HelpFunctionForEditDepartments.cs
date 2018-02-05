using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEmpAcc.Models.Departments
{
    public static class HelpFunctionForEditDepartments
    {
        public static void ChangeHeadOfSector(
            ref ApplicationUser newHead,
            ref ApplicationUser oldHead,
            ref Sector sec
            )
        {
            oldHead.AccessLvl = 1;
            oldHead.Branch = null;
            oldHead.Department = null;
            oldHead.Sector = null;

            newHead.AccessLvl = 2; //2 access level for head of sector
            newHead.Sector = sec.Name;
            newHead.Branch = sec.BrnchName;
            newHead.Department = sec.DprtmntName;

            sec.HeadId = newHead.Id;
        }

        public static void ChangeHeadOfBranch(
           ref ApplicationUser newHead,
           ref ApplicationUser oldHead,
           ref Branch brc
           )
        {
            oldHead.AccessLvl = 1;
            oldHead.Branch = null;
            oldHead.Department = null;
            oldHead.Sector = null;

            newHead.AccessLvl = 3; //3 access level for head of branch
            newHead.Sector = null;
            newHead.Branch = brc.Name;
            newHead.Department = brc.DprtmntName;

            brc.HeadId = newHead.Id;
        }

        public static void ChangeHeadOfDepartment(
           ref ApplicationUser newHead,
           ref ApplicationUser oldHead,
           ref Department dep
           )
        {
            oldHead.AccessLvl = 1;
            oldHead.Branch = null;
            oldHead.Department = null;
            oldHead.Sector = null;

            newHead.AccessLvl = 4; //4 access level for head of department
            newHead.Sector = null;
            newHead.Branch = null;
            newHead.Department = dep.Name;

            dep.HeadId = newHead.Id;
        }

        public static void AddHeadOfSector(
            ref ApplicationUser newHead,
            ref Sector sector
            )
        {
            newHead.AccessLvl = 2; //2 access level for head of sector
            newHead.Sector = sector.Name;
            newHead.Branch = sector.BrnchName;
            newHead.Department = sector.DprtmntName;

            sector.IsHead = true;
            sector.HeadId = newHead.Id;
        }

        public static void AddHeadOfBranch(
            ref ApplicationUser newHead,
            ref Branch branch
            )
        {
            newHead.AccessLvl = 3; //3 access level for head of branch
            newHead.Sector = null;
            newHead.Branch = branch.Name;
            newHead.Department = branch.DprtmntName;

            branch.IsHead = true;
            branch.HeadId = newHead.Id;
        }

        public static void AddHeadOfDepartment(
            ref ApplicationUser newHead,
            ref Department department
            )
        {
            newHead.AccessLvl = 4; //4 access level for head of department
            newHead.Sector = null;
            newHead.Branch = null;
            newHead.Department = department.Name;

            department.IsHead = true;
            department.HeadId = newHead.Id;
        }

        public static void RemoveHeadOfSector(
            ref ApplicationUser oldHead,
            ref Sector sector
            )
        {
            oldHead.AccessLvl = 1;
            oldHead.Branch = null;
            oldHead.Department = null;
            oldHead.Sector = null;

            sector.IsHead = false;
            sector.HeadId = null;
        }

        public static void RemoveHeadOfBranch(
            ref ApplicationUser oldHead,
            ref Branch branch
            )
        {
            oldHead.AccessLvl = 1;
            oldHead.Branch = null;
            oldHead.Department = null;

            branch.IsHead = false;
            branch.HeadId = null;
        }

        public static void RemoveHeadOfDepartment(
            ref ApplicationUser oldHead,
            ref Department department
            )
        {
            oldHead.AccessLvl = 1;
            oldHead.Department = null;

            department.IsHead = false;
            department.HeadId = null;
        }

        public static void ChangeBranchOfSector(
            ref Sector sector,
            Branch branch
            )
        {
            sector.BrnchId = branch.Id;
            sector.BrnchName = branch.Name;
            sector.DprtmntId = branch.DprtmntId;
            sector.DprtmntName = branch.DprtmntName;
        }

        public static void ChangeDepartmentOfBranch(
            ref Branch branch,
            Department department
            )
        {
            branch.DprtmntId = department.Id;
            branch.DprtmntName = department.Name;
        }

        
    }
}
