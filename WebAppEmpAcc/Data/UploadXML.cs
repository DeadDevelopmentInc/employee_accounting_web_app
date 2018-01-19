using Microsoft.AspNetCore.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebAppEmpAcc.Models.DepartmentModels;

namespace WebAppEmpAcc.Data
{
    public static class UploadXML
    {
        public static List<Department> Departments { get; set; }

        private static XmlSerializer formatter = new XmlSerializer(typeof(List<Department>));
        
        public static void ReadXML()
        {
            using (FileStream fs = new FileStream("StructOfDepartment.xml", FileMode.OpenOrCreate))
            {
                Departments = (List<Department>)formatter.Deserialize(fs);
            }
        }

        public static void SaveChangeInXML()
        {
            using (FileStream fs = new FileStream("StructOfDepartment.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Departments);
            }
        }

    }
}
