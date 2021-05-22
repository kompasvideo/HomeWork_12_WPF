using HomeWork_WPF.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_WPF.Providers
{
    class EmployeeProvider : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Экземпляр сотрудника
        /// </summary>
        private Employee employee;
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string FirstName
        {
            get { return employee.FirstName; }
            set
            {
                employee.FirstName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.FirstName)));
            }
        }
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string LastName
        {
            get { return employee.LastName; }
            set
            {
                employee.LastName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.LastName)));
            }
        }
        /// <summary>
        /// Возраст сотрудника
        /// </summary>
        public int Age
        {
            get { return employee.Age; }
            set
            {
                employee.Age = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Age)));
            }
        }
        /// <summary>
        /// Должность сотрудника
        /// </summary>
        public EnEmployee EEmployee
        {
            get { return employee.EEmployee; }
            set { employee.EEmployee = value; }
        }
        /// <summary>
        /// Id демартамента
        /// </summary>
        public uint DepartmentId 
        { 
            get { return employee.DepartmentId; }
            set { employee.DepartmentId = value; }
        }
        public string DepartmentName
        {
            get 
            {
                return Repository.GetNameDepartment(DepartmentId);
            }
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="employee"></param>
        public EmployeeProvider(Employee employee)
        {
            this.employee = employee;
        }

        /// <summary>
        /// Возвращяет экземпляр сотрудника
        /// </summary>
        /// <returns></returns>
        public Employee GetEmployee()
        {
            return employee;
        }
        /// <summary>
        /// Возвращяет номер отдела (департамента)
        /// </summary>
        /// <returns></returns>
        public uint GetDepartmentId()
        {
            return DepartmentId;
        }
    }
}
