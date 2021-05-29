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
        System.ComponentModel.ICollectionView Source;
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.DataModel = new Model();
            treeView.ItemsSource = this.DataModel.GetDepartments();            
            this.Source = CollectionViewSource.GetDefaultView(Model.repository.Employees);
            WPFDataGrid.ItemsSource = this.Source;
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
            Source.Filter = new Predicate<object>(this.DataModel.myFilter);
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
            Source = CollectionViewSource.GetDefaultView(Model.repository.Employees);
            WPFDataGrid.ItemsSource = Source;
            Source.Filter = new Predicate<object>(this.DataModel.myFilter);
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
                return this.DataModel.AvailableDevelopment;
            }
        }
    }
}
