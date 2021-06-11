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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HomeWork_WPF.ViewModels;

namespace HomeWork_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Подписывается на сообщение ReturnAddWorker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<Employee>(MainViewModel.ReturnAddWorker);
            Messenger.Default.Register<Department>(MainViewModel.ReturnAddDepartment);
        }
        /// <summary>
        /// Отписывается от сообщение ReturnAddWorker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<Employee>(MainViewModel.ReturnAddWorker);
            Messenger.Default.Unregister<Department>(MainViewModel.ReturnAddDepartment);
        }
        /// <summary>
        /// Получаем MainViewModel для установки фильтра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WPFDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //MyCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(WPFDataGrid.DataContext);
            MainViewModel.Source = (ListCollectionView)CollectionViewSource.GetDefaultView(WPFDataGrid.ItemsSource);
            MainViewModel.Source.Filter = new Predicate<object>(MainViewModel.myFilter);
        }
    }
}
