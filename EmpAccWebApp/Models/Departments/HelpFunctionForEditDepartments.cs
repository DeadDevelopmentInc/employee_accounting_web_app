using EmpAccWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAccWebApp.Models.Departments
{
    public static class HelpFunctionForEditDepartments
    {
        /// <summary>
        /// Function for change params of sector head
        /// </summary>
        /// <param name="newHead">new head of sector</param>
        /// <param name="oldHead">old head of sector</param>
        /// <param name="sec">the sector</param>
        public static void ChangeHeadOfSector(
            ref ApplicationUser newHead,
            ref ApplicationUser oldHead,
            ref Sector sec
            )
        {
            //Clear position of old head
            oldHead.AccessLvl = 1;

            //Update information of new head
            newHead.AccessLvl = 2; //2 access level for head of sector
            //Save id
            sec.HeadId = newHead.Id;
        }

        /// <summary>
        /// Function for change params of branch head
        /// </summary>
        /// <param name="newHead">new head of branch</param>
        /// <param name="oldHead">old head of branch</param>
        /// <param name="brc">the branch</param>
        public static void ChangeHeadOfBranch(
           ref ApplicationUser newHead,
           ref ApplicationUser oldHead,
           ref Branch brc
           )
        {
            //Clear position of old head
            oldHead.AccessLvl = 1;
            //Update information of new head
            newHead.AccessLvl = 3; //3 access level for head of branch
            //Save new id
            brc.HeadId = newHead.Id;
        }

        /// <summary>
        /// Function for change params of department head
        /// </summary>
        /// <param name="newHead">new head of branch</param>
        /// <param name="oldHead">old head of branch</param>
        /// <param name="brc">the branch</param>
        public static void ChangeHeadOfDepartment(
           ref ApplicationUser newHead,
           ref ApplicationUser oldHead,
           ref Department dep
           )
        {
            //Clear position of old head
            oldHead.AccessLvl = 1;
            //Update information of new head
            newHead.AccessLvl = 4; //4 access level for head of department
            //Save new id
            dep.HeadId = newHead.Id;
        }

        /// <summary>
        /// Function for add head of sector
        /// </summary>
        /// <param name="newHead">new head</param>
        /// <param name="sector">the sector</param>
        public static void AddHeadOfSector(
            ref ApplicationUser newHead,
            ref Sector sector
            )
        {
            //Update information of head
            newHead.AccessLvl = 2; //2 access level for head of sector
            sector.HeadId = newHead.Id;
        }

        /// <summary>
        /// Function for add head of branch
        /// </summary>
        /// <param name="newHead">new head</param>
        /// <param name="sector">the branch</param>
        public static void AddHeadOfBranch(
            ref ApplicationUser newHead,
            ref Branch branch
            )
        {
            //Update information of head
            newHead.AccessLvl = 3; //3 access level for head of branch
            branch.HeadId = newHead.Id;
        }

        /// <summary>
        /// Function for add head of department
        /// </summary>
        /// <param name="newHead">new head</param>
        /// <param name="sector">the deparment</param>
        public static void AddHeadOfDepartment(
            ref ApplicationUser newHead,
            ref Department department
            )
        {
            //Update information of head
            newHead.AccessLvl = 4; //4 access level for head of department
            department.HeadId = newHead.Id;
        }

        /// <summary>
        /// Function for remove head of sector
        /// </summary>
        /// <param name="oldHead">the head</param>
        /// <param name="sector">the sector</param>
        public static void RemoveHeadOfSector(
            ref ApplicationUser oldHead,
            ref Sector sector
            )
        {
            oldHead.AccessLvl = 1;
            
            sector.HeadId = null;
        }

        /// <summary>
        /// Function for remove head of branch
        /// </summary>
        /// <param name="oldHead">the head</param>
        /// <param name="sector">the branch</param>
        public static void RemoveHeadOfBranch(
            ref ApplicationUser oldHead,
            ref Branch branch
            )
        {
            oldHead.AccessLvl = 1;
            
            branch.HeadId = null;
        }

        /// <summary>
        /// Function for remove head of department
        /// </summary>
        /// <param name="oldHead">the head</param>
        /// <param name="sector">the department</param>
        public static void RemoveHeadOfDepartment(
            ref ApplicationUser oldHead,
            ref Department department
            )
        {
            oldHead.AccessLvl = 1;
            
            department.HeadId = null;
        }

        /// <summary>
        /// Function for change branch for sector
        /// </summary>
        /// <param name="sector">the sector</param>
        /// <param name="branch">new branch</param>
        public static void ChangeBranchOfSector(
            ref Sector sector,
            Branch branch
            )
        {
            sector.BrnchId = branch.Id;
        }

        /// <summary>
        /// Function for change department for branch
        /// </summary>
        /// <param name="branch">the branch</param>
        /// <param name="department">new department</param>
        public static void ChangeDepartmentOfBranch(
            ref Branch branch,
            Department department
            )
        {
            branch.DprtmntId = department.Id;
        }

        
    }
}
