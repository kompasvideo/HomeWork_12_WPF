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
        /// <summary>
        /// Ссылка на Model
        /// </summary>
        private Model DataModel { get; set; }

        /// <summary>
        /// Показывает выбрана ли должность
        /// </summary>
        bool vacancy;


        // свойство - изменён отдел
        public bool ChangedDepartment { get; set; }

        // старый DepartmentId отдела
        public uint DepartmentOldId { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="worker"></param>
        public EditWorkerWindow(Model DataModel)
        {
            InitializeComponent();
            this.DataModel = DataModel;
            grid1.DataContext = this.DataModel.GetSelectEmployeeProvider();
            lbEmployees.ItemsSource = this.DataModel.employeesList;
            lbEmployees.SelectedIndex = this.DataModel.GetEmployeeVacancy();
            vacancy = false;
        }


        /// <summary>
        /// Нажата кнопка "ОК"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bOK_Click(object sender, RoutedEventArgs e)
        {
            this.DataModel.SelectEmployeeEdit(vacancy);
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
        /// Нажата кнопка "Изменить отдел"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Select_Click(object sender, RoutedEventArgs e)
        {
            SelectDepartmentWindow selectDepartmentWindow = new SelectDepartmentWindow(this.DataModel);
            selectDepartmentWindow.ShowDialog();
        }

        /// <summary>
        /// Смена должности
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vacancy = true;
            this.DataModel.SetNewVacancy(e.AddedItems[0]);
        }
    }
}
