using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeWork_WPF.Employees;

namespace HomeWork_WPF
{
    public class Department 
    {
        // департаменты
        protected List<Department> departments;

        public Department()
        {
            departments = new List<Department>();
        }
        public Department(string Name, uint DepartmentId, ObservableCollection<Department> Departments)
        {
            this.Name = Name;
            this.DepartmentId = DepartmentId;
            this.Departments = Departments;
        }

        #region Свойства
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отдел Id
        /// </summary>
        public uint DepartmentId { get; set; }

        /// <summary>
        /// Отдел
        /// </summary>
        public ObservableCollection<Department> Departments { get; set; }

        // руководитель
        public Manager manager { get; set; }        
        #endregion
    }
}
