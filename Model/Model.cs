using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HomeWork_WPF.Departments;
using HomeWork_WPF.Employees;
using HomeWork_WPF.Providers;

namespace HomeWork_WPF
{
    public class Model
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
        DepartmentNameProvider departmentNameProvider;
        /// <summary>
        /// выбранный TreeViewItem вдиалоговых окнах 
        /// </summary>
        Department selectDialog;

        private static uint departmentId;

        private Employee newEmployee;
        private Employee selectedEmployee;
        EmployeeProvider employeeProvider;
        public string[] employeesList = { "Руководитель", "Рабочий", "Интерн" };

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
            newEmployee = new Worker("имя", "фамилия", 30, 0);
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
            if (Repository.Departments == null) return 0;
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
        /// <summary>
        /// Возвращяет выбранный отдел(департамент) в диалогах
        /// </summary>
        /// <returns></returns>
        public Department GetSelectDialog()
        {
            return selectDialog;
        }
        /// <summary>
        /// Сохранение нового выбранного отдела(департамента) в диалогах
        /// </summary>
        /// <param name="e"></param>
        public void SetSelectDialog(object e)
        {            
            selectDialog = (Department)e;
            if (employeeProvider != null)
                employeeProvider.DepartmentId = selectDialog.DepartmentId;
        }
        /// <summary>
        /// Добавить отдел(департамент)
        /// </summary>
        /// <param name="name"></param>
        public void AddDepartment(string name)
        {
            if (selectDialog.Departments == null)
            {
                selectDialog.Departments = new ObservableCollection<Department>();
            }
            departmentId = 0;
            selectDialog.Departments.Add(new Department(name,
                GetNextDepartmentId(Repository.Departments) + 1, new ObservableCollection<Department>()));
        }
        /// <summary>
        /// Получает наибольший DepartmentId
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        private uint GetNextDepartmentId(ObservableCollection<Department> departments)
        {
            foreach (var dep in departments)
            {
                if (dep.DepartmentId > departmentId) departmentId = dep.DepartmentId;
                if (dep.Departments.Count > 0) GetNextDepartmentId(dep.Departments);
            }
            return departmentId;
        }

        /// <summary>
        /// Удаляет отдел и находящихся в нём сотрудников рекурсивно
        /// </summary>
        /// <param name="select"></param>
        public void DeleteDepartmentAndWorkers(Department select)
        {
            // получаем список подчинённых отделов
            ObservableCollection<Department> l_departments = select.Departments;

            // получаем Id отдела
            uint departmentId = select.DepartmentId;

            while (true)
            {
                // получаем позицию в списке сотрудников 
                bool poisk = false;
                int count = repository.Employees.Count;
                for (int i = 0; i < count; i++)
                {
                    if (repository.Employees[i].DepartmentId == departmentId)
                    {
                        // и удаляем его
                        poisk = true;
                        repository.Employees.Remove(repository.Employees[i]);
                        break;
                    }
                }
                if (!poisk) break;
            }
            DeleteDepartment(Repository.Departments, select);

            // проходимся по подчинённым отделам
            foreach (var dep in l_departments)
            {
                DeleteDepartmentAndWorkers(dep);
            }
        }

        /// <summary>
        /// Удаляет отдел
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="select"></param>
        void DeleteDepartment(ObservableCollection<Department> departments, Department select)
        {
            bool poisk = false;
            foreach (var dep in departments)
            {
                if (dep.DepartmentId == select.DepartmentId)
                {
                    poisk = true;
                    break;
                }
                ObservableCollection<Department> l_departments = dep.Departments;
                if (l_departments.Count > 0)
                {
                    DeleteDepartment(l_departments, select);
                }
            }
            if (poisk)
                departments.Remove(select);
        }
        /// <summary>
        /// Возвращяет имя отдела(департамента)
        /// </summary>
        /// <returns></returns>
        public object GetDepartmentName()
        {
            if (departmentNameProvider == null)
                departmentNameProvider = new DepartmentNameProvider(selectDialog.Name);
            departmentNameProvider.Name = selectDialog.Name;
            return departmentNameProvider;
        }
        /// <summary>
        /// Изменяет имя отдела(департамента)
        /// </summary>
        public void SetDepartmentName()
        {
            selectDialog.Name = departmentNameProvider.Name;
        }
        /// <summary>
        /// Привязывает поля выбранного сотудника к EmployeeProvider
        /// </summary>
        /// <param name="e"></param>
        public void SelectEmployee(object e)
        {
            selectedEmployee = e as Employee;
            if (selectedEmployee == null)
            {
                MessageBox.Show("Ошибка 1", "Ошибка");
                return;
            }
        }
        /// <summary>
        /// Привязывает поля нового сотудника к EmployeeProvider
        /// </summary>
        /// <returns></returns>
        public object GetNewEmployeeProvider()
        {
            if (employeeProvider == null)
                employeeProvider = new EmployeeProvider(newEmployee);
            return employeeProvider;
        }
        /// <summary>
        /// Привязывает поля выбранного сотудника к EmployeeProvider
        /// </summary>
        /// <returns></returns>
        public object GetSelectEmployeeProvider()
        {
            employeeProvider = new EmployeeProvider(selectedEmployee);
            return employeeProvider;
        }
        /// <summary>
        /// Выбор должности при добавлении нового сотрудника
        /// </summary>
        /// <param name="e"></param>
        public void SetNewVacancy(object e)
        {
            string str = e as String;
            switch (str)
            {
                case "Руководитель":
                    employeeProvider.EEmployee = EnEmployee.Manager;
                    break;
                case "Рабочий":
                    employeeProvider.EEmployee = EnEmployee.Worker;
                    break;
                case "Интерн":
                    employeeProvider.EEmployee = EnEmployee.Intern;
                    break;
                default:
                    MessageBox.Show("Выберите сначала должность сотрудника", "Добавить сотрудника");
                    return;
            }
        }
        /// <summary>
        /// Возвращяет экземпляр нового сотрудника
        /// </summary>
        /// <returns></returns>
        public Employee GetNewEmployee()
        {
            uint depId = selectDialog.DepartmentId; 
            Employee employee = employeeProvider.GetEmployee();
            switch (employee.EEmployee)
            {
                case EnEmployee.Manager:
                    employee = new Manager(employee.FirstName, employee.LastName, employee.Age, depId);
                    break;
                case EnEmployee.Worker:
                    employee = new Worker(employee.FirstName, employee.LastName, employee.Age, depId);
                    break;
                case EnEmployee.Intern:
                    employee = new Intern(employee.FirstName, employee.LastName, employee.Age, depId);
                    break;
            }
            return employee;
        }
        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="e"></param>
        public void DeleteEmployee(object e)
        {
            if (e == null)
            {
                MessageBox.Show("Сначала выделите сотрудника для удаления", "Удалить сотрудника");
                return;
            }
            Employee worker = (Employee)e;
            if (MessageBox.Show($"Удалить сотрудника {worker.FirstName} {worker.LastName}", "Удалить сотрудника", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            Model.repository.Employees.Remove(worker);
        }
        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        /// <param name="e"></param>
        public void EditEmployee(object e)
        {
            if (e == null)
            {
                MessageBox.Show("Сначала выделите сотрудника для редактирования", "Редактировать сотрудника");
                return;
            }
            Employee worker = (Employee)e;
            EditWorkerWindow editWorkerWindow = new EditWorkerWindow(this);
            if (editWorkerWindow.ShowDialog() == true)
            { }    
        }
        /// <summary>
        /// Получить должность сотрудника
        /// </summary>
        /// <returns></returns>
        public int GetEmployeeVacancy()
        {
            #region скрыто
            //switch (selectedEmployee.EEmployee)
            //{
            //    case EnEmployee.Manager:
            //        return 0;
            //    case EnEmployee.Worker:
            //        return 1;
            //    case EnEmployee.Intern:
            //        return 2;
            //}
            //return -1;
            #endregion
            switch (employeeProvider.EEmployee)
            {
                case EnEmployee.Manager:
                    return 0;
                case EnEmployee.Worker:
                    return 1;
                case EnEmployee.Intern:
                    return 2;
            }
            return -1;
        }
        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        /// <param name="vacancy"></param>
        public void SelectEmployeeEdit( bool vacancy)
        {
            if (vacancy)
            {
                Employee employee = null;
                switch (selectedEmployee.EEmployee)
                {
                    case EnEmployee.Manager:
                        employee = new Manager(selectedEmployee.FirstName, selectedEmployee.LastName, selectedEmployee.Age, selectedEmployee.DepartmentId);
                        break;
                    case EnEmployee.Worker:
                        employee = new Worker(selectedEmployee.FirstName, selectedEmployee.LastName, selectedEmployee.Age, selectedEmployee.DepartmentId);
                        break;
                    case EnEmployee.Intern:
                        employee = new Intern(selectedEmployee.FirstName, selectedEmployee.LastName, selectedEmployee.Age, selectedEmployee.DepartmentId);
                        break;
                }
                repository.Employees.Remove(selectedEmployee);
                repository.Employees.Add(employee);
            }
            else
            {
                selectedEmployee.FirstName = employeeProvider.FirstName;
                selectedEmployee.LastName = employeeProvider.LastName;
                selectedEmployee.Age = employeeProvider.Age;
                selectedEmployee.DepartmentId = employeeProvider.DepartmentId;
                selectedEmployee.EEmployee = employeeProvider.EEmployee;
            }
        }
        public IEnumerable<string> AvailableDevelopment
        {
            get
            {
                return (from developer in repository.Employees
                        select developer.Job).Distinct();
            }
        }
    }
}
