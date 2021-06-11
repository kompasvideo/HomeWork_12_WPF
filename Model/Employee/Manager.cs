using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_WPF.Employees
{
    public class Manager:Employee
    {
        public Manager(string firstName, string lastName, int age, uint departmentId, string job = "Руководитель") : 
            base(firstName, lastName, age, departmentId, job)
        {
            EEmployee = EnEmployee.Руководитель;
        }
    }
}
