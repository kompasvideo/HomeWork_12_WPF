using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_WPF
{
    public enum EnEmployee: int
    {
        Employee,
        Worker,
        Intern,
        Manager
    }
    public abstract class Employee : INotifyPropertyChanged
    {
        string firstName;
        string lastName;
        int salary;
        int age;
        uint departmentId;
        string job;
        EnEmployee eEmployee;

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public virtual int Salary
        {
            get { return salary; }
            set
            {
                salary = value;
                OnPropertyChanged("Salary");
            }
        }
        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }
        public uint DepartmentId
        {
            get { return departmentId; }
            set
            {
                departmentId = value;
                OnPropertyChanged("DepartmentId");
            }
        }
        public string Job
        {
            get { return job; }
            set
            {
                job = value;
                OnPropertyChanged("Job");
            }
        }
        public EnEmployee EEmployee
        {
            get { return eEmployee; }
            set
            {
                eEmployee = value;
            }
        }


        public Employee(string firstName, string lastName, int age, uint departmentId, string job)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            DepartmentId = departmentId;
            Job = job;
            eEmployee = EnEmployee.Employee;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public override string ToString()
        {
            return string.Format($"{LastName} {FirstName}");
        }
        /// <summary>
        /// Сортировка по окладу
        /// </summary>
        public class SortBySalary : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                Employee X = (Employee)x;
                Employee Y = (Employee)y;
                if (X.Salary == Y.Salary) return 0;
                else if (X.Salary > Y.Salary) return 0;
                else return -1;
            }
        }
        public class SortByAge : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                Employee X = (Employee)x;
                Employee Y = (Employee)y;
                if (X.Age == Y.Age) return 0;
                else if (X.Age > Y.Age) return 0;
                else return -1;
            }
        }
        public class SortByFirstName : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                Employee X = (Employee)x;
                Employee Y = (Employee)y;

                return String.Compare(X.FirstName, Y.FirstName);
            }
        }
        public class SortByLastName : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                Employee X = (Employee)x;
                Employee Y = (Employee)y;

                return String.Compare(X.LastName, Y.LastName);
            }
        }
        public class SortByDepartment : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                Employee X = (Employee)x;
                Employee Y = (Employee)y;
                string x_str = Repository.GetNameDepartment(X.DepartmentId);
                string y_str = Repository.GetNameDepartment(Y.DepartmentId);
                return String.Compare(x_str, y_str);
            }
        }
        public class SortByEmployee : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                Employee X = (Employee)x;
                Employee Y = (Employee)y;
                string x_str = X.EEmployee.ToString();
                string y_str = Y.EEmployee.ToString();
                return String.Compare(x_str, y_str);
            }
        }
    }
}
