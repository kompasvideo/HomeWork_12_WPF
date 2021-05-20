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
    /// Логика взаимодействия для SelectDepartmentWindow.xaml
    /// </summary>
    public partial class SelectDepartmentWindow : Window
    {
        ObservableCollection<Department> departments;

        // выбранный TreeViewItem 
        Department select;

        public SelectDepartmentWindow(ObservableCollection<Department> departments)
        {
            InitializeComponent();
            this.departments = departments;
            treeView.ItemsSource = departments;
        }

        /// <summary>
        /// Нажата кнопка "ОК"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bOK_Click(object sender, RoutedEventArgs e)
        {
            if (select.Name == null)
            {
                MessageBox.Show("Выберите сначала отдел");
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
        /// Обрабатывает событие treeView_SelectedItemChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            select = (Department)e.NewValue;
        }

        /// <summary>
        /// Возвращяет отдел в виде структуры
        /// </summary>
        /// <returns></returns>
        public Department GetDepartment()
        {
            return select;
        }
    }
}
