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
using HomeWork_WPF.Employees;
using HomeWork_WPF.Interface;
using HomeWork_WPF.ViewModels;

namespace HomeWork_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для AddWorkerWindow.xaml
    /// </summary>
    public partial class AddWorkerWindow : Window, IAddWorker
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public AddWorkerWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Реализация метода Show из интерфейса IAddWorker
        /// </summary>
        /// <param name="selectDepartment"></param>
        public void Show(Department selectDepartment)
        {
            DataContext = new AddWorkerViewModel(new Worker("Имя", "Фамилия", 25, 0, "Рабочий"), selectDepartment);
            this.ShowDialog();
        }
    }
}
