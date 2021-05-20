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
using System.Windows.Shapes;

namespace HomeWork_WPF.Departments
{
    /// <summary>
    /// Логика взаимодействия для DelDepartmentWindow.xaml
    /// </summary>
    public partial class DelDepartmentWindow : Window
    {
        // Список содрудников
        ObservableCollection<Employee> Employees;

        // Список отделов
        ObservableCollection<Department> departments;

        // выбранный TreeViewItem 
        Department select;

        public DelDepartmentWindow(ObservableCollection<Department> departments, ObservableCollection<Employee> Employees)
        {
            InitializeComponent();
            this.departments = departments;
            this.Employees = Employees;
            treeView.ItemsSource = departments;
        }

        /// <summary>
        /// Нажата кнопка "ОК"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bOK_Click(object sender, RoutedEventArgs e)
        {
            if (select.DepartmentId > 0)
                DeleteDepartmentAndWorkers(select);
            this.Close();
        }

        /// <summary>
        /// Удаляет отдел и находящихся в нём сотрудников рекурсивно
        /// </summary>
        /// <param name="select"></param>
        void DeleteDepartmentAndWorkers(Department select)
        {
            // получаем список подчинённых отделов
            ObservableCollection<Department> l_departments = select.Departments;

            // получаем Id отдела
            uint departmentId = select.DepartmentId;

            while (true)
            {
                // получаем позицию в списке сотрудников 
                bool poisk = false;
                int count = Employees.Count;
                for (int i = 0; i < count; i++)
                {
                    if (Employees[i].DepartmentId == departmentId)
                    {
                        // и удаляем его
                        poisk = true;
                        Employees.Remove(Employees[i]);
                        break;
                    }
                }
                if (!poisk) break;
            }
            DeleteDepartment(departments, select);

            // проходимся по подчинённым отделам
            foreach (var dep in l_departments)
            {
                DeleteDepartmentAndWorkers(dep);
            }
        }

        /// <summary>
        /// Удаляет отдел
        /// </summary>
        /// <param name="departments"></param>
        /// <param name="select"></param>
        void DeleteDepartment(ObservableCollection<Department> departments, Department select)
        {
            bool poisk = false;
            foreach (var dep in departments)
            {
                if (dep.DepartmentId == select.DepartmentId)
                {
                    poisk = true;
                    break;
                }
                ObservableCollection<Department> l_departments = dep.Departments;
                if (l_departments.Count > 0)
                {
                    DeleteDepartment(l_departments, select);
                }
            }
            if (poisk)
                departments.Remove(select);
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
        /// Обрабатывает событие treeView_SelectedItemChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            select = (Department)e.NewValue;
        }
    }
}
