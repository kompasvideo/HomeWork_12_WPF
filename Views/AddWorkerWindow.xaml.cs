using HomeWork_WPF.Departments;
using System;
using System.Collections.Generic;
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

namespace HomeWork_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для AddWorkerWindow.xaml
    /// </summary>
    public partial class AddWorkerWindow : Window
    {
        /// <summary>
        /// Ссылка на Model
        /// </summary>
        private Model DataModel { get; set; }

        /// <summary>
        /// Показывает выбран ли отдел 
        /// </summary>
        bool department;
        /// <summary>
        /// Показывает выбрана ли должность
        /// </summary>
        bool vacancy;

        public AddWorkerWindow()
        {
            InitializeComponent();
            this.DataModel = DataModel;
            //grid1.DataContext = this.DataModel.GetNewEmployeeProvider();
            //lbEmployees.ItemsSource = this.DataModel.employeesList;
            department = false;
            vacancy = false;
        }

        /// <summary>
        /// Нажата кнопка "ОК"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bOK_Click(object sender, RoutedEventArgs e)
        {
            if (!department)
            {
                MessageBox.Show("Ошибка. Не выбран отдел", "Добавить сотрудника");
                return;
            }
            if (!vacancy)
            {
                MessageBox.Show("Ошибка. Не выбрана должность", "Добавить сотрудника");
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
        /// Нажата кнопка "Выбрать отдел"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Select_Click(object sender, RoutedEventArgs e)
        {
            SelectDepartmentWindow selectDepartmentWindow = new SelectDepartmentWindow(this.DataModel);
            if (selectDepartmentWindow.ShowDialog() == true)
            {
                department = true;
            }
        }

        /// <summary>
        /// Выбран список должностей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbEmployees_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            vacancy = true;
            this.DataModel.SetNewVacancy(e.AddedItems[0]);
        }
    }
}
