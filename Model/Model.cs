using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeWork_WPF.Departments;
using HomeWork_WPF.Employees;

namespace HomeWork_WPF
{
    class Model
    {
        /// Организация
        public static Repository repository { get; set; }

        /// <summary>
        /// сумма оклада руководителя департамента
        /// </summary>
        static int salary = 0;
        // выбранный TreeViewItem 
        Department select;
        SelectProvider selectProvider;

        /// <summary>
        /// Статический конструктор без параметров
        /// </summary>
        static Model()
        {
            // создаём организацию
            repository = new Repository();
        }
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Model()
        {
            select = GetDepartment(0);
        }
        /// <summary>
        /// Возвращяет экземпляр класса Department
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Department GetDepartment(int index)
        {
            return Repository.Departments[index];
        }
        /// <summary>
        /// Возвращяет коллекцию ObservableCollection<Department>
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Department> GetDepartments()
        {
            return Repository.Departments;
        }

        /// <summary>
        /// Возвращяет коллекцию ObservableCollection<Employee>
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Employee> GetEmployees()
        {
            return repository.Employees;
        }

        /// <summary>
        /// Подсчитывает оклад руководителя департамента данног департамента
        /// </summary>
        /// <param name="p_depId"></param>
        public static void SetSalary(uint p_depId)
        {
            foreach (var emp in repository.Employees)
            {
                if (emp.DepartmentId == p_depId)
                {
                    if (emp.GetType() == typeof(Worker))
                    {
                        salary += emp.Salary * 8 * 23;
                    }
                    if (emp.GetType() == typeof(Intern))
                    {
                        salary += emp.Salary;
                    }
                }
            }
        }

        /// <summary>
        /// Подсчитывает оклад руководителя департамента
        /// </summary>
        /// <param name="p_depId"></param>
        /// <returns></returns>
        public static int GetSalary(uint p_depId)
        {
            salary = 0;
            foreach (var dep in Repository.Departments)
            {
                if (dep.DepartmentId == p_depId)
                    SearchDepartment(p_depId, dep.Departments, true);
                else
                    SearchDepartment(p_depId, dep.Departments, false);
            }
            return salary;
        }

        /// <summary>
        /// Ищет дочерние департаменты 
        /// </summary>
        /// <param name="p_depId"></param>
        /// <param name="departments"></param>
        /// <param name="child"></param>
        private static void SearchDepartment(uint p_depId, ObservableCollection<Department> departments, bool child)
        {
            foreach (var dep in departments)
            {
                if (dep.DepartmentId == p_depId)
                {
                    SetSalary(p_depId);
                    if (dep.Departments != null)
                    {
                        foreach (var depR in dep.Departments)
                        {
                            SearchDepartment(p_depId, dep.Departments, true);
                        }
                    }
                }
                else
                {
                    if (child)
                    {
                        SetSalary(p_depId);
                    }
                    if (dep.Departments != null)
                    {
                        SearchDepartment(p_depId, dep.Departments, child);
                    }
                }
            }
        }
        /// <summary>
        /// Возвращяет выбранный отдел(департамент)
        /// </summary>
        /// <returns></returns>
        public object GetSelect()
        {
            if (selectProvider == null)
                selectProvider = new SelectProvider(select.manager);
            return selectProvider;
        }

        /// <summary>
        /// Фильтр для сортировки сотрудников по отделу(департаменту)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool myFilter(object obj)
        {
            if (select.DepartmentId == 0) return true;
            else
            {
                var e = obj as Employee;
                if (e != null)
                {
                    if (select.DepartmentId == e.DepartmentId) return true;
                }
                return false;
            }
        }
        /// <summary>
        /// Сохранение нового выбранного отдела(департамента)
        /// </summary>
        /// <param name="e"></param>
        public void SetSelect(object e)
        {
            Department selectL = (Department)e;
            select = selectL;
            selectProvider.manager = selectL.manager;
        }
    }
}
