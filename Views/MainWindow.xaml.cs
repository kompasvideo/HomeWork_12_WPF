using HomeWork_WPF.Departments;
using HomeWork_WPF.Employees;
using HomeWork_WPF.ViewModels;
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
using System.Runtime.Serialization.Json;

namespace HomeWork_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// CollectionViewSource для департаментов
        /// </summary>
        System.ComponentModel.ICollectionView Source;
        MainViewModel mainViewModel;
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //treeView.ItemsSource = ((App)Application.Current).DataModel.Departments;
            mainViewModel = new MainViewModel();
            treeView.ItemsSource = mainViewModel.Departments;
            //this.Source = CollectionViewSource.GetDefaultView(((App)Application.Current).DataModel.Employees);
            this.Source = CollectionViewSource.GetDefaultView(mainViewModel.Employees);
            WPFDataGrid.ItemsSource = this.Source;
            //FIO.DataContext = Model.GetSelect();
            FIO.DataContext = mainViewModel.SelectedEmployee;
            //Salary.DataContext = Model.GetSelect();
            Salary.DataContext = mainViewModel.SelectedEmployee;
        }

        
        /// <summary>
        /// Обработчик выделения департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //Model.SetSelect(e.NewValue);
            Department dp = mainViewModel.SelectDepartment;
            Source.Filter = new Predicate<object>(Model.myFilter);
        }
        
        
        /// <summary>
        /// Выбран пункт меню "Добавить отдел"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDepartment_Click(object sender, RoutedEventArgs e)
        {
            AddDepartmentWindow addDepartmentWindow = new AddDepartmentWindow();
            addDepartmentWindow.ShowDialog();
        }
        /// <summary>
        /// Выбран пункт меню "Удалить отдел"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelDepartment_Click(object sender, RoutedEventArgs e)
        {
            DelDepartmentWindow delDepartmentWindow = new DelDepartmentWindow();
            delDepartmentWindow.ShowDialog();
        }
        /// <summary>
        /// Выбран пункт меню "Редактировать отдел"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditDepartment_Click(object sender, RoutedEventArgs e)
        {
            EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow();
            if(editDepartmentWindow.ShowDialog() == true)
            {
                Model.SetDepartmentName();
                treeView.Items.Refresh();
            }
        }        
        
        /// <summary>
        /// Выбран пункт меню "Удалить сотрудника"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelWorker_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).DataModel.DeleteEmployee(WPFDataGrid.SelectedItem);
        }
        
        /// <summary>
        /// Выбран пункт меню "Редактировать сотрудника"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditWorker_Click(object sender, RoutedEventArgs e)
        {
            Model.EditEmployee( WPFDataGrid.SelectedItem);            
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
            ((App)Application.Current).DataModel.Sort(newName);
            Source = CollectionViewSource.GetDefaultView(((App)Application.Current).DataModel.Employees);
            WPFDataGrid.ItemsSource = Source;
            Source.Filter = new Predicate<object>(Model.myFilter);
        }

        /// <summary>
        /// Выбор нового сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WPFDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                Model.SelectEmployee(e.AddedItems[0]);
        }
        private void ListView_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader currentHeader = e.OriginalSource as GridViewColumnHeader;
            if (currentHeader != null && currentHeader.Role != GridViewColumnHeaderRole.Padding)
            {
                //using (this.Source.DeferRefresh())
                //{
                //    Func<SortDescription, bool> lamda = item => item.PropertyName.Equals(currentHeader.Column.Header.ToString());
                //    if (this.Source.SortDescriptions.Count(lamda) > 0)
                //    {
                //        SortDescription currentSortDescription = this.Source.SortDescriptions.First(lamda);
                //        ListSortDirection sortDescription = currentSortDescription.Direction == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;


                //        currentHeader.Column.HeaderTemplate = currentSortDescription.Direction == ListSortDirection.Ascending ?
                //            this.Resources["HeaderTemplateArrowDown"] as DataTemplate : this.Resources["HeaderTemplateArrowUp"] as DataTemplate;

                //        this.Source.SortDescriptions.Remove(currentSortDescription);
                //        this.Source.SortDescriptions.Insert(0, new SortDescription(currentHeader.Column.Header.ToString(), sortDescription));
                //    }
                //    else
                //        this.Source.SortDescriptions.Add(new SortDescription(currentHeader.Column.Header.ToString(), ListSortDirection.Ascending));
                //}


            }


        }
        public IEnumerable<string> DeveloperList
        {
            get
            {
                //return ((App)Application.Current).DataModel.AvailableDevelopment;
                return mainViewModel.AvailableDevelopment;
            }
        }
    }
}
