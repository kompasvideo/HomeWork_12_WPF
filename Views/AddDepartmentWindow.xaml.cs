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
using HomeWork_WPF.Interface;
using HomeWork_WPF.ViewModels;

namespace HomeWork_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для AddDepartmentWindow.xaml
    /// </summary>
    public partial class AddDepartmentWindow : Window, IAddDepartment
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public AddDepartmentWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Реализация метода Show из интерфейса IAddDepartment
        /// </summary>
        /// <param name="selectDepartment"></param>
        /// <param name="id"></param>
        public void Show(Department selectDepartment, uint id)
        {
            DataContext = new AddDepartmentViewModel(new Department("Департамент", id,
                new ObservableCollection<Department>()), selectDepartment);
            this.ShowDialog();
        }
    }
}
