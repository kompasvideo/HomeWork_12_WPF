using HomeWork_WPF.Departments;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HomeWork_WPF.Employees
{
    /// <summary>
    /// Логика взаимодействия для EditWorkerWindow.xaml
    /// </summary>
    public partial class EditWorkerWindow : Window
    {
        // сотрудник
        public Employee worker;

        // отдел
        ObservableCollection<Department> departments;

        // выбранный отдел
        Department select;

        // свойство - изменён отдел
        public bool ChangedDepartment { get; set; }

        // старый DepartmentId отдела
        public uint DepartmentOldId { get; set; }

        // ссылка на класс организации
        Repository repository;

        string[] employees = { "Руководитель", "Рабочий", "Интерн" };

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="worker"></param>
        public EditWorkerWindow(Employee worker, Repository repository)
        {
            InitializeComponent();
            this.worker = worker;
            this.departments = Repository.Departments;
            this.repository = repository;
            lbEmployees.ItemsSource = employees;
            switch(worker.EEmployee)
            {
                case EnEmployee.Manager:
                    lbEmployees.SelectedIndex = 0;
                    break;
                case EnEmployee.Worker:
                    lbEmployees.SelectedIndex = 1;
                    break;
                case EnEmployee.Intern:
                    lbEmployees.SelectedIndex = 2;
                    break;
            }
            SetProperty();
        }

        void SetProperty()
        {
            tbLastName.Text = worker.LastName;
            tbFirstName.Text = worker.FirstName;
            tbAge.Text = worker.Age.ToString();
            tbDepartament.Content = Repository.GetNameDepartment(worker.DepartmentId);
            DepartmentOldId = worker.DepartmentId;
        }

        /// <summary>
        /// Нажата кнопка "ОК"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bOK_Click(object sender, RoutedEventArgs e)
        {
            worker.LastName = tbLastName.Text;
            worker.FirstName = tbFirstName.Text;
            int age;
            if (! int.TryParse(tbAge.Text, out age))
            {
                MessageBox.Show("Ошибка при вводе возраста сотрудника. Должно быть число", "Редактировать сотрудника");
                return;
            }
            else
            {
                worker.Age = age;
            }
            if (select != null)
            {
                if (select.Name == null)
                {
                    // отдел не изменён
                    ChangedDepartment = false;
                }
                else
                {
                    if (select.DepartmentId == 0)
                    {
                        MessageBox.Show("Ошибка. Не выбран отдел", "Добавить сотрудника");
                        return;
                    }
                    ChangedDepartment = true;
                    worker.DepartmentId = select.DepartmentId;
                }
            }
            switch (lbEmployees.SelectedItem)
            {
                case "Руководитель":
                    if (worker.EEmployee != EnEmployee.Manager)
                    {
                        repository.Employees.Remove(worker);
                        worker = new Manager(worker.FirstName, worker.LastName, worker.Age, worker.DepartmentId);
                        repository.Employees.Add(worker);
                    }
                    break;
                case "Рабочий":
                    if (worker.EEmployee != EnEmployee.Worker)
                    {
                        repository.Employees.Remove(worker);
                        worker = new Worker(worker.FirstName, worker.LastName, worker.Age, worker.DepartmentId);
                        repository.Employees.Add(worker);
                    }
                    break;
                case "Интерн":
                    if (worker.EEmployee != EnEmployee.Intern)
                    {
                        repository.Employees.Remove(worker);
                        worker = new Intern(worker.FirstName, worker.LastName, worker.Age, worker.DepartmentId);
                        repository.Employees.Add(worker);
                    }
                    break;
                default:
                    MessageBox.Show("Выберите сначала должность сотрудника", "Добавить сотрудника");
                    return;

            }
            DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Нажата кнопка "Отмена"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Возвращяет структуру worker
        /// </summary>
        /// <returns></returns>
        public Employee GetWorker()
        {
            return worker;
        }

        /// <summary>
        /// Нажата кнопка "Изменить отдел"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Select_Click(object sender, RoutedEventArgs e)
        {
            SelectDepartmentWindow selectDepartmentWindow = new SelectDepartmentWindow(departments);
            if (selectDepartmentWindow.ShowDialog() == true)
            {
                select = selectDepartmentWindow.GetDepartment();
                tbDepartament.Content = select.Name;
            }
        }        
    }
}
