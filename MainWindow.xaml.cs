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
        private Model DataModel
        {
            get;
            set;
        }
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
            AddDepartmentWindow addDepartmentWindow = new AddDepartmentWindow(Repository.Departments);
            addDepartmentWindow.ShowDialog();
        }
        /// <summary>
        /// Выбран пункт меню "Удалить отдел"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelDepartment_Click(object sender, RoutedEventArgs e)
        {
            DelDepartmentWindow delDepartmentWindow = new DelDepartmentWindow(Repository.Departments, Model.repository.Employees);
            delDepartmentWindow.ShowDialog();
            WPFDataGrid.ItemsSource = Model.repository.Employees;
        }
        /// <summary>
        /// Выбран пункт меню "Редактировать отдел"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditDepartment_Click(object sender, RoutedEventArgs e)
        {
            EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow(Repository.Departments);
            editDepartmentWindow.ShowDialog();
        }

        /// <summary>
        /// Выбран пункт меню "Добавить сотрудника"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddWorker_Click(object sender, RoutedEventArgs e)
        {
            AddWorkerWindow addWorkerWindow = new AddWorkerWindow(Repository.Departments);
            if (addWorkerWindow.ShowDialog() == true)
            {
                Model.repository.Employees.Add(addWorkerWindow.GetWorker());
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
            var select = WPFDataGrid.SelectedItem;
            if (select == null)
            {
                MessageBox.Show("Сначала выделите сотрудника для удаления", "Удалить сотрудника");
                return;
            }
            Employee worker = (Employee)select;
            if (MessageBox.Show($"Удалить сотрудника {worker.FirstName} {worker.LastName}", "Удалить сотрудника", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            Model.repository.Employees.Remove(worker);
        }
        
        /// <summary>
        /// Выбран пункт меню "Редактировать сотрудника"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditWorker_Click(object sender, RoutedEventArgs e)
        {
            var select = WPFDataGrid.SelectedItem;
            if (select == null)
            {
                MessageBox.Show("Сначала выделите сотрудника для редактирования", "Редактировать сотрудника");
                return;
            }
            Employee worker = (Employee)select;
            EditWorkerWindow editWorkerWindow = new EditWorkerWindow(worker, Model.repository);
            if (editWorkerWindow.ShowDialog() == true)
            {
                //// редактируем сотрудника
                //Worker workerEdit = editWorkerWindow.GetWorker();
                //for (int i = 0; i < l_Workers.Count; i++)
                //{
                //    if (workerEdit.Nomer == l_Workers[i].Nomer)
                //    {
                //        Workers.Remove(l_Workers[i]);
                //        Workers.Add(workerEdit);
                //        l_Workers.Remove(l_Workers[i]);
                //        l_Workers.Add(workerEdit);
                //        break;
                //    }
                //}

                //// проверяем изменён ли отдел
                //if (editWorkerWindow.ChangedDepartment)
                //{
                //    // увеличиваем количество сотрудников нового отдела
                //    DepartmentStruct selectDS = editWorkerWindow.GetDepartment();
                //    for (int i = 0; i < departments.Count; i++)
                //    {
                //        if (selectDS.DepartmentId == departments[i].DepartmentId)
                //        {
                //            departments[i].AddWorker();
                //        }
                //    }

                //    // Уменьшаем количество сотрудников в бывшем отделе
                //    for (int i = 0; i < departments.Count; i++)
                //    {
                //        if (editWorkerWindow.DepartmentOldId == departments[i].DepartmentId)
                //        {
                //            departments[i].DelWorker();
                //        }
                //    }
                //}
            }
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
        }
    }
}
