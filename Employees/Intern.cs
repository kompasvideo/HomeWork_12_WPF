using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_WPF.Employees
{
    class Intern : Employee
    {
        public Intern(string firstName, string lastName, int age, uint departmentId, string job = "Интерн") :
            base(firstName, lastName, age, departmentId, job)
        {
            EEmployee = EnEmployee.Intern;
        }
        public override int Salary
        {
            get
            {
                return 500;
            }
        }
    }
}
