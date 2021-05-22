using HomeWork_WPF.Departments;
using HomeWork_WPF.Employees;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HomeWork_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Ссылка на Model
        /// </summary>
        private Model DataModel { get; set; }
        /// <summary>
        /// CollectionViewSource для департаментов
        /// </summary>
        System.ComponentModel.ICollectionView myView;
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.DataModel = new Model();
            treeView.ItemsSource = this.DataModel.GetDepartments();
            myView = CollectionViewSource.GetDefaultView(this.DataModel.GetEmployees());
            WPFDataGrid.ItemsSource = myView;
            FIO.DataContext = this.DataModel.GetSelect();
            Salary.DataContext = this.DataModel.GetSelect();
        }

        
        /// <summary>
        /// Обработчик выделения департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.DataModel.SetSelect(e.NewValue);
            myView.Filter = new Predicate<object>(this.DataModel.myFilter);
        }
        
        
        /// <summary>
        /// Выбран пункт меню "Добавить отдел"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDepartment_Click(object sender, RoutedEventArgs e)
        {
            AddDepartmentWindow addDepartmentWindow = new AddDepartmentWindow(this.DataModel);
            addDepartmentWindow.ShowDialog();
        }
        /// <summary>
        /// Выбран пункт меню "Удалить отдел"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelDepartment_Click(object sender, RoutedEventArgs e)
        {
            DelDepartmentWindow delDepartmentWindow = new DelDepartmentWindow(this.DataModel);
            delDepartmentWindow.ShowDialog();
        }
        /// <summary>
        /// Выбран пункт меню "Редактировать отдел"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditDepartment_Click(object sender, RoutedEventArgs e)
        {
            EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow(this.DataModel);
            if(editDepartmentWindow.ShowDialog() == true)
            {
                this.DataModel.SetDepartmentName();
                treeView.Items.Refresh();
            }
        }

        /// <summary>
        /// Выбран пункт меню "Добавить сотрудника"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddWorker_Click(object sender, RoutedEventArgs e)
        {
            AddWorkerWindow addWorkerWindow = new AddWorkerWindow(this.DataModel);
            if (addWorkerWindow.ShowDialog() == true)
            {
                Model.repository.Employees.Add(this.DataModel.GetNewEmployee());
                Salary.DataContext = this.DataModel.GetSelect();
            }
        }
        
        /// <summary>
        /// Выбран пункт меню "Удалить сотрудника"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelWorker_Click(object sender, RoutedEventArgs e)
        {
            this.DataModel.DeleteEmployee(WPFDataGrid.SelectedItem);
        }
        
        /// <summary>
        /// Выбран пункт меню "Редактировать сотрудника"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditWorker_Click(object sender, RoutedEventArgs e)
        {
            this.DataModel.EditEmployee( WPFDataGrid.SelectedItem);            
        }

        /// <summary>
        /// Выбран пункт меню "Сортировать -> ..."
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            // получаем имя пункта меню
            string name = mi.Name;
            // удаляеи из имени последний символ
            string newName = name.Substring(0, name.Length - 1);
            Model.repository.Sort(newName);
            myView = CollectionViewSource.GetDefaultView(Model.repository.Employees);
            WPFDataGrid.ItemsSource = myView;
            myView.Filter = new Predicate<object>(this.DataModel.myFilter);
        }

        /// <summary>
        /// Выбор нового сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WPFDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                this.DataModel.SelectEmployee(e.AddedItems[0]);
        }
    }
}
