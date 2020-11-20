using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using System.Configuration;
using LotusTimesheet.Class;

namespace LotusTimesheet
{
    class Program
    {
        static void Main(string[] args)
        {
            var siteUrl = ConfigurationManager.AppSettings["SP_Address_Live"];
            GetEmployeeFromEasyHR getEmployeeFromEasyHR = new GetEmployeeFromEasyHR();

            List<EmployeeModel> employeeModels = new List<EmployeeModel>();

            EmployeeModel emp = new EmployeeModel();
            employeeModels = getEmployeeFromEasyHR.GetEmployeAttendance();
            Console.WriteLine("Please Wait...");

            Console.WriteLine("Dont Stop program....");
            for (var i = 0; i < employeeModels.Count; i++)
            {
              emp = getEmployeeFromEasyHR.CheckNewEntry(employeeModels[i].empno, siteUrl);
                Console.WriteLine("Please Wait...");
                if (emp.count == 0)
                {
                    getEmployeeFromEasyHR.InsertEntry(employeeModels[i], siteUrl);

                }
                else
                {
                    getEmployeeFromEasyHR.UpdateEntry(employeeModels[i], siteUrl, emp.ID);

                }

            }





            getEmployeeFromEasyHR.CheckNewEntry("", siteUrl);
        }
    }
}
