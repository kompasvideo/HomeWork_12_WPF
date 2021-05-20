using HomeWork_WPF.Departments;
using System.Collections.ObjectModel;
using System.Windows;

namespace HomeWork_WPF.Employees
{
    /// <summary>
    /// Логика взаимодействия для AddWorkerWindow.xaml
    /// </summary>
    public partial class AddWorkerWindow : Window
    {
        // Структура сотрудника
        public Employee worker;

        // Структура отделов
        ObservableCollection<Department> departments;

        // выбранный отдел
        Department select;
        string[] employees = {"Руководитель", "Рабочий", "Интерн"};

        public AddWorkerWindow(ObservableCollection<Department> departments)
        {
            InitializeComponent();
            this.departments = departments;
            lbEmployees.ItemsSource = employees;
        }

        /// <summary>
        /// Нажата кнопка "ОК"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bOK_Click(object sender, RoutedEventArgs e)
        {
            if (select.Name == null || select.DepartmentId == 0)
            {
                MessageBox.Show("Ошибка. Не выбран отдел", "Добавить сотрудника");
                return;
            }
            int age;
            if (! int.TryParse(tbAge.Text, out age))
            {
                MessageBox.Show("Ошибка при вводе возраста сотрудника. Должно быть число", "Добавить сотрудника");
                return;
            }

            switch (lbEmployees.SelectedItem)
            {
                case "Руководитель":
                    worker = new Manager(tbFirstName.Text, tbLastName.Text, age, select.DepartmentId);
                    break;
                case "Рабочий":
                    worker = new Worker(tbFirstName.Text, tbLastName.Text, age, select.DepartmentId);
                    break;
                case "Интерн":
                    worker = new Intern(tbFirstName.Text, tbLastName.Text, age, select.DepartmentId);
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
        /// Возвращяет структуру Employee
        /// </summary>
        /// <returns></returns>
        public Employee GetWorker()
        {
            return worker;
        }        

        /// <summary>
        /// Нажата кнопка "Выбрать отдел"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Select_Click(object sender, RoutedEventArgs e)
        {
            SelectDepartmentWindow selectDepartmentWindow = new SelectDepartmentWindow(departments);
            if (selectDepartmentWindow.ShowDialog() == true)
            {
                select = selectDepartmentWindow.GetDepartment();
            }
        }
    }
}
