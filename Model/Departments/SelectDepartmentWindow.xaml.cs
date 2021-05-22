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
        /// <summary>
        /// Ссылка на Model
        /// </summary>
        private Model DataModel { get; set; }

        /// <summary>
        /// Показывает выбран ли отдел 
        /// </summary>
        bool department;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="DataModel"></param>
        public SelectDepartmentWindow(Model DataModel)
        {
            InitializeComponent();
            this.DataModel = DataModel;
            treeView.ItemsSource = this.DataModel.GetDepartments();
            department = false;
        }

        /// <summary>
        /// Нажата кнопка "ОК"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bOK_Click(object sender, RoutedEventArgs e)
        {
            if (! department)
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
            this.DataModel.SetSelectDialog(e.NewValue);
            department = true;
        }
    }
}
