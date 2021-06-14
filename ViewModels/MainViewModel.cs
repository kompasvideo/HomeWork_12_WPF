using DevExpress.Mvvm;
using HomeWork_WPF.Employees;
using HomeWork_WPF.Interface;
using HomeWork_WPF.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace HomeWork_WPF.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        // Список отделов
        public ObservableCollection<Department> Departments { get; set; }
        // Список сотрудников
        public static ObservableCollection<Employee> Employees { get; set; }
        // Список сотрудников для считывания из Json
        private List<Worker> _listEmployees;
        private int _iterator;
        /// <summary>
        /// Выбранный Employee в ListView
        /// </summary>
        public Employee SelectedEmployee { get; set; }
        /// <summary>
        /// Руководитель выделенного отдела
        /// </summary>
        public Employee SelectedManager { get; set; }
        /// <summary>
        /// Выбранный Department в TreeView
        /// </summary>
        public static Department SelectDepartment { get; set; }
        /// <summary>
        /// CollectionViewSource для департаментов
        /// </summary>
        public static System.ComponentModel.ICollectionView Source;

        /// <summary>
        /// Конструктор
        /// </summary>
        public MainViewModel()
        {
            LoadJson();
            SelectedManager = Departments[0].manager;
        }
        /// <summary>
        /// Считывает департаменты и сотрудников из файлов json
        /// </summary>
        public void LoadJson()
        {
            string json = File.ReadAllText("departments.json");
            Departments = JsonConvert.DeserializeObject<ObservableCollection<Department>>(json);
            json = File.ReadAllText("employees.json");
            Employees = new ObservableCollection<Employee>();
            _listEmployees = JsonConvert.DeserializeObject<List<Worker>>(json);
            ListToObservableCollection();
            SaveManager(Departments);
            SaveSalaryToManager();
        }
        /// <summary>
        /// Разбивает сотрудников из json на классы
        /// </summary>
        private void ListToObservableCollection()
        {
            foreach (var emp in _listEmployees)
            {
                switch (emp.EEmployee)
                {
                    case EnEmployee.Intern:
                        Intern intern = new Intern(emp.FirstName, emp.LastName, emp.Age, emp.DepartmentId);
                        Employees.Add(intern);
                        break;
                    case EnEmployee.Worker:
                        Worker worker = new Worker(emp.FirstName, emp.LastName, emp.Age, emp.DepartmentId);
                        Employees.Add(worker);
                        break;
                    case EnEmployee.Manager:
                        Manager manager = new Manager(emp.FirstName, emp.LastName, emp.Age, emp.DepartmentId);
                        Employees.Add(manager);
                        break;
                    default:
                        break;
                }
            }
        }
        private void SaveToJson()
        {
            string json = JsonConvert.SerializeObject(Departments);
            File.WriteAllText("departments.json", json);
            json = JsonConvert.SerializeObject(Employees);
            File.WriteAllText("employees.json", json);
        }
        /// <summary>
        /// Назначает департаменту руководителя 
        /// </summary>
        /// <param name="p_departments"></param>
        void SaveManager(ObservableCollection<Department> p_departments)
        {
            foreach (var dep in p_departments)
            {
                dep.manager = Employees[_iterator++] as Manager;
                if (dep.Departments != null) SaveManager(dep.Departments);
            }
        }
        /// <summary>
        /// Записать зарплату в руководителя
        /// </summary>
        void SaveSalaryToManager()
        {
            foreach (var dep in Departments)
            {
                int salary = 0;
                salary += SearchDepartment(dep.Departments, dep.DepartmentId);
                GetSalary(salary, dep.DepartmentId);
            }
        }
        /// <summary>
        /// Ищет дочерние департаменты 
        /// </summary>
        /// <param name="p_depId"></param>
        /// <param name="departments"></param>
        /// <param name="child"></param>
        private int SearchDepartment(ObservableCollection<Department> departments, uint departmentId)
        {
            int salary = 0;
            foreach (var dep in departments)
            {
                salary += SearchDepartment(dep.Departments, dep.DepartmentId);
            }
            return GetSalary(salary, departmentId);
        }

        /// <summary>
        /// Возвращяет Salary руководителя
        /// </summary>
        /// <param name="salary"></param>
        /// <returns></returns>
        private int GetSalary(int salary, uint departmentId)
        {
            List<Employee> manager = new List<Employee>();
            foreach (var e in Employees)
            {
                if (e.DepartmentId == departmentId)
                {
                    switch (e.EEmployee)
                    {
                        case EnEmployee.Intern:
                            salary += e.Salary;
                            break;
                        case EnEmployee.Worker:
                            salary += e.Salary * 8 * 23;
                            break;
                        case EnEmployee.Manager:
                            manager.Add(e);
                            break;
                    }
                }
            }
            if (manager.Count > 0)
            {
                foreach (var l_manager in manager)
                {
                    l_manager.Salary = salary;
                }
            }
            return salary;
        }

        /// <summary>
        /// Фильтр для сортировки сотрудников по отделу(департаменту)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool myFilter(object obj)
        {
            if (SelectDepartment == null) return true;
            if (SelectDepartment.DepartmentId == 0) return true;
            else
            {
                var e = obj as Employee;
                if (e != null)
                {
                    if (SelectDepartment.DepartmentId == e.DepartmentId) return true;
                }
                return false;
            }
        }
        /// <summary>
        /// Возвращяет коллекцию для WPFDataGrid
        /// </summary>
        /// <returns></returns>
        public System.Collections.IEnumerable GetSource()
        {
            Source = CollectionViewSource.GetDefaultView(Employees);
            return Source;
        }
        public IEnumerable<string> AvailableDevelopment
        {
            get
            {
                return (from developer in Employees
                        select developer.Job).Distinct();
            }
        }
        /// <summary>
        /// Используется в GetNextDepartmentId()
        /// </summary>
        private static uint departmentId;
        /// <summary>
        /// Получает наибольший DepartmentId
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        private static uint GetNextDepartmentId(ObservableCollection<Department> departments)
        {
            foreach (var dep in departments)
            {
                if (dep.DepartmentId > departmentId) departmentId = dep.DepartmentId;
                if (dep.Departments.Count > 0) GetNextDepartmentId(dep.Departments);
            }
            return departmentId;
        }
        /// <summary>
        /// Команда "Добавить сотрудника"
        /// </summary>
        public ICommand AddWorker_Click
        {
            get
            {
                var a  = new DelegateCommand((obj) =>
                {
                    if (SelectDepartment == null)
                        SelectDepartment = Departments[0];
                    IAddWorker addWorkerWindow = new AddWorkerWindow();
                    addWorkerWindow.Show(SelectDepartment);
                });
                return a; 
            }
        }
        /// <summary>
        /// Возвращяет Employee из диалогового окна AddWorker
        /// </summary>
        /// <param name="employee"></param>
        public static void ReturnAddWorker(Employee employee)
        {
            Employees.Add(employee);          
        }
        /// <summary>
        /// Команда "Удалить сотрудника"
        /// </summary>
        public ICommand DelWorker_Click
        {
            get
            {
                var a = new DelegateCommand((obj) =>
                {
                    if(SelectedEmployee == null)
                    { 
                        MessageBox.Show("Сначала выделите сотрудника для удаления", "Удалить сотрудника");
                        return;
                    }                    
                    if (MessageBox.Show($"Удалить сотрудника {SelectedEmployee.FirstName} {SelectedEmployee.LastName}", "Удалить сотрудника", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        return;
                    Employees.Remove(SelectedEmployee);
                });
                return a;
            }
        }
        /// <summary>
        /// Команда "Выбор элемента в TreeView"
        /// </summary>
        public ICommand SelectedItemChangedCommand
        {
            get
            {                
                var a = new DelegateCommand((obj) =>
                {
                    SelectDepartment = (Department)obj;
                    SelectedManager = SelectDepartment.manager;
                    if (Source != null)
                        Source.Filter = new Predicate<object>(myFilter);
                });
                return a;
            }            
        }
        /// <summary>
        /// Возвращяет коллекцию для ComboBox в ListView
        /// </summary>
        public IEnumerable<string> DeveloperList
        {
            get
            {
                return (from developer in Employees
                        select developer.Job).Distinct();
            }
        }
        /// <summary>
        /// Команда "Добавить отдел"
        /// </summary>
        public ICommand AddDepartment_Click
        {
            get
            {
                var a = new DelegateCommand((obj) =>
                {
                    if (SelectDepartment == null)
                        SelectDepartment = Departments[0];
                    IAddDepartment addDepartmentWindow = new AddDepartmentWindow();
                    addDepartmentWindow.Show(SelectDepartment,
                        GetNextDepartmentId(Departments) + 1);
                });
                return a;
            }
        }
        /// <summary>
        /// Возвращяет Department из диалогового окна AddDepartment
        /// </summary>
        /// <param name="employee"></param>
        public static void ReturnAddDepartment(Department department)
        {
            SelectDepartment.Departments.Add(department);
        }
        /// <summary>
        /// Команда "Удалить отдел"
        /// </summary>
        public ICommand DelDepartment_Click
        {
            get
            {
                var a = new DelegateCommand((obj) =>
                {
                    if (SelectDepartment == null)
                    {
                        MessageBox.Show("Сначала выделите отдел", "Удалить отдел");
                        return;
                    }
                    if (MessageBox.Show($"Удалить отдел {SelectDepartment.Name}", "Удалить отдел", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        return;
                    // получаем список подчинённых отделов
                    ObservableCollection<Department> l_departments = SelectDepartment.Departments;

                    // получаем Id отдела
                    uint departmentId = SelectDepartment.DepartmentId;

                    while (true)
                    {
                        // получаем позицию в списке сотрудников 
                        bool poisk = false;
                        int count = Employees.Count;
                        for (int i = 0; i < count; i++)
                        {
                            if (Employees[i].DepartmentId == departmentId)
                            {
                                // и удаляем его
                                poisk = true;
                                Employees.Remove(Employees[i]);
                                break;
                            }
                        }
                        if (!poisk) break;
                    }
                    DeleteDepartment(Departments, SelectDepartment);

                    // проходимся по подчинённым отделам
                    foreach (var dep in l_departments)
                    {
                        DeleteDepartmentAndWorkers(dep);
                    }
                });
                return a;
            }
        }
        /// <summary>
        /// Удаляет отдел
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="select"></param>
        static void DeleteDepartment(ObservableCollection<Department> departments, Department select)
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
                int count = Employees.Count;
                for (int i = 0; i < count; i++)
                {
                    if (Employees[i].DepartmentId == departmentId)
                    {
                        // и удаляем его
                        poisk = true;
                        Employees.Remove(Employees[i]);
                        break;
                    }
                }
                if (!poisk) break;
            }
            DeleteDepartment(Departments, select);

            // проходимся по подчинённым отделам
            foreach (var dep in l_departments)
            {
                DeleteDepartmentAndWorkers(dep);
            }
        }
        DelegateCommand sortClick;
        /// <summary>
        /// Команда "Сортировать"
        /// </summary>
        public ICommand Sort_Click
        {
            get
            {
                if (sortClick == null)
                {
                    sortClick = new DelegateCommand((obj) =>
                    {
                        string name = (string)obj;
                        List<Employee> _listSortEmployees = Employees.ToList();
                        switch (name)
                        {
                            case "LastName":
                                _listSortEmployees.Sort(Employee.SortedBy(SortedCriterion.LastName));
                                break;
                            case "FirstName":
                                _listSortEmployees.Sort(Employee.SortedBy(SortedCriterion.FirstName));
                                break;
                            case "Employee":
                                _listSortEmployees.Sort(Employee.SortedBy(SortedCriterion.Employee));
                                break;
                            case "Age":
                                _listSortEmployees.Sort(Employee.SortedBy(SortedCriterion.Age));
                                break;
                            case "Department":
                                _listSortEmployees.Sort(Employee.SortedBy(SortedCriterion.Department));
                                break;
                            case "Salary":
                                _listSortEmployees.Sort(Employee.SortedBy(SortedCriterion.Salary));
                                break;
                        }
                        Employees.Clear();
                        foreach (var l in _listSortEmployees)
                        {
                            Employees.Add(l);
                        }
                        Source.Filter = new Predicate<object>(myFilter);
                    });
                }
                return sortClick;
            }
        }
    }
}
