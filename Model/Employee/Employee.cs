using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_WPF
{
    /// <summary>
    /// Объединение, позволяет получить тип класса из Employee
    /// </summary>
    public enum EnEmployee
    {
        Employee,
        Worker,
        Intern,
        Manager
    }

    /// <summary>
    /// Критерий сортировки
    /// </summary>
    public enum SortedCriterion
    {
        FirstName,
        LastName,
        Age,
        Salary,
        Department,
        Employee
    }

    public abstract class Employee : INotifyPropertyChanged
    {
        string firstName;
        string lastName;
        protected int salary;
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

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="age"></param>
        /// <param name="departmentId"></param>
        /// <param name="job"></param>
        public Employee(string firstName, string lastName, int age, uint departmentId, string job)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            DepartmentId = departmentId;
            Job = job;
            eEmployee = EnEmployee.Employee;
        }        

        /// <summary>
        /// Событие PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Обработчик события PropertyChanged
        /// </summary>
        /// <param name="info"></param>
        protected void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        /// <summary>
        /// Вернуть как строку
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format($"{LastName} {FirstName}");
        }
        /// <summary>
        /// Сортировка по окладу
        /// </summary>
        private class SortBySalary : IComparer<Employee>
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
        /// <summary>
        /// Сортировка по возрасту
        /// </summary>
        private class SortByAge : IComparer<Employee>
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
        /// <summary>
        /// Сортировка по FirstName
        /// </summary>
        private class SortByFirstName : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                Employee X = (Employee)x;
                Employee Y = (Employee)y;

                return String.Compare(X.FirstName, Y.FirstName);
            }
        }
        /// <summary>
        /// Сортировка по вLastName
        /// </summary>
        private class SortByLastName : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                Employee X = (Employee)x;
                Employee Y = (Employee)y;

                return String.Compare(X.LastName, Y.LastName);
            }
        }
        /// <summary>
        /// Сортировка по имени отдела (департамента)
        /// </summary>
        private class SortByDepartment : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                Employee X = (Employee)x;
                Employee Y = (Employee)y;
                if (X.DepartmentId == Y.DepartmentId) return 0;
                else if (X.DepartmentId > Y.DepartmentId) return 0;
                else return -1;
            }
        }
        /// <summary>
        /// Сортировка по должности
        /// </summary>
        private class SortByEmployee : IComparer<Employee>
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

        /// <summary>
        /// Сортировка по критерию
        /// </summary>
        public static IComparer<Employee> SortedBy(SortedCriterion Criterion)
        {
            switch (Criterion)
            {
                case SortedCriterion.FirstName:
                    return new SortByFirstName();
                case SortedCriterion.LastName:
                    return new SortByFirstName();
                case SortedCriterion.Age:
                    return new SortByAge();
                case SortedCriterion.Salary:
                    return new SortBySalary();
                case SortedCriterion.Department:
                    return new SortByDepartment();
            }
            return new SortByEmployee();
        }
    }
}
