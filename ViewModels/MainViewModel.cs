using DevExpress.Mvvm;
using HomeWork_WPF.Employees;
using HomeWork_WPF.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HomeWork_WPF.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        // Список отделов
        public ObservableCollection<Department> Departments { get; set; }
        // Список сотрудников
        public ObservableCollection<Employee> Employees { get; set; }
        // Список сотрудников для считывания из Json
        private List<Worker> _listEmployees;
        private static int _iterator;

        public Employee  SelectedEmployee { get; set; }
        public Department  SelectDepartment { get; set; }

        public MainViewModel()
        {
            LoadJson();
        }
        /// <summary>
        /// Считывает департаменты и сотрудников из файлов json
        /// </summary>
        public void LoadJson()
        {
            string json = File.ReadAllText("departments.json");
            Departments = JsonConvert.DeserializeObject<ObservableCollection<Department>>(json);
            json = File.ReadAllText("employees.json");
            //Employees = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(json);
            Employees = new ObservableCollection<Employee>();
            _listEmployees = JsonConvert.DeserializeObject<List<Worker>>(json);
            ListToObservableCollection();
            SaveManager(Departments);
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
                    case EnEmployee.Интерн:
                        Intern intern = new Intern(emp.FirstName, emp.LastName, emp.Age, emp.DepartmentId);
                        Employees.Add(intern);
                        break;
                    case EnEmployee.Рабочий:
                        Worker worker = new Worker(emp.FirstName, emp.LastName, emp.Age, emp.DepartmentId);
                        Employees.Add(worker);
                        break;
                    case EnEmployee.Руководитель:
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
        public IEnumerable<string> AvailableDevelopment
        {
            get
            {
                return (from developer in Employees
                        select developer.Job).Distinct();
            }
        }

        public ICommand AddWorker_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    AddWorkerWindow addWorkerWindow = new AddWorkerWindow();
                    if (addWorkerWindow.ShowDialog() == true)
                    {
                        //Model.repository.Employees.Add(this.DataModel.GetNewEmployee());
                        //Salary.DataContext = this.DataModel.GetSelect();
                    }
                });
            }
        }
    }
}
