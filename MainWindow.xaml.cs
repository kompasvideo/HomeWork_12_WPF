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
using HomeWork_WPF.Departments;
using HomeWork_WPF.Employees;

namespace HomeWork_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// Организация
        static Repository repository;
        // выбранный TreeViewItem 
        Department select;
        /// <summary>
        /// сумма оклада руководителя департамента
        /// </summary>
        static int salary = 0;
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
            // создаём организацию
            repository = new Repository();

            select = Repository.Departments[0];
            treeView.ItemsSource = Repository.Departments;
            myView = CollectionViewSource.GetDefaultView(repository.Employees);
            WPFDataGrid.ItemsSource = myView;
            FIO.DataContext = new SelectProvider(select);
            Salary.DataContext = new SelectProvider(select);
        }

        /// <summary>
        /// Фильтр для показа сотрудников данного департамента
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool myFilter(object obj)
        {
            if (select.DepartmentId == 0) return true;
            else
            {
                var e = obj as Employee;
                if (e != null)
                {
                    if (select.DepartmentId == e.DepartmentId) return true;
                }
                return false;
            }
        }
        
        /// <summary>
        /// Обработчик выделения департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            select = (Department)e.NewValue;
            FIO.DataContext = new SelectProvider(select);
            Salary.DataContext = new SelectProvider(select);
            myView.Filter = new Predicate<object>(myFilter);
        }
        
        /// <summary>
        /// Подсчитывает оклад руководителя департамента данног департамента
        /// </summary>
        /// <param name="p_depId"></param>
        public static void SetSalary(uint p_depId)
        {
            foreach (var emp in repository.Employees)
            {
                if (emp.DepartmentId == p_depId)
                {
                    if (emp.GetType() == typeof(Worker))
                    {
                        salary += emp.Salary * 8 * 23;
                    }
                    if (emp.GetType() == typeof(Intern))
                    {
                        salary += emp.Salary;
                    }
                }
            }           
        }

        /// <summary>
        /// Подсчитывает оклад руководителя департамента
        /// </summary>
        /// <param name="p_depId"></param>
        /// <returns></returns>
        public static int GetSalary(uint p_depId)
        {
            salary = 0;
            foreach (var dep in Repository.Departments)
            {
                if (dep.DepartmentId == p_depId)
                    SearchDepartment(p_depId, dep.Departments, true);
                else
                    SearchDepartment(p_depId, dep.Departments,false);
            }
            return salary;
        }

        /// <summary>
        /// Ищет дочерние департаменты 
        /// </summary>
        /// <param name="p_depId"></param>
        /// <param name="departments"></param>
        /// <param name="child"></param>
        private static void SearchDepartment(uint p_depId, ObservableCollection<Department> departments,bool child)
        {            
            foreach (var dep in departments)
            {
                if (dep.DepartmentId == p_depId)
                {
                    SetSalary(p_depId);
                    if (dep.Departments != null)
                    {
                        foreach (var depR in dep.Departments)
                        {
                            SearchDepartment(p_depId, dep.Departments, true);
                        }
                    }
                }
                else
                {
                    if (child)
                    {
                        SetSalary(p_depId);
                    }
                    if (dep.Departments != null)
                    {
                        SearchDepartment(p_depId, dep.Departments, child);
                    }
                }
            }
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
            DelDepartmentWindow delDepartmentWindow = new DelDepartmentWindow(Repository.Departments, repository.Employees);
            delDepartmentWindow.ShowDialog();
            WPFDataGrid.ItemsSource = repository.Employees;
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
                repository.Employees.Add(addWorkerWindow.GetWorker());
                Salary.DataContext = new SelectProvider(select);
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
            repository.Employees.Remove(worker);
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
            EditWorkerWindow editWorkerWindow = new EditWorkerWindow(worker, repository);
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
            repository.Sort(newName);
            myView = CollectionViewSource.GetDefaultView(repository.Employees);
            WPFDataGrid.ItemsSource = myView;
        }
    }
}
