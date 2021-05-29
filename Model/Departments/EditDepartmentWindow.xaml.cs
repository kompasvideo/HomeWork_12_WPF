﻿using System;
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
    /// Логика взаимодействия для EditDepartmentWindow.xaml
    /// </summary>
    public partial class EditDepartmentWindow : Window
    {
        private Model DataModel
        {
            get;
            set;
        }
        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// <param name="departments"></param>
        public EditDepartmentWindow(Model DataModel)
        {
            InitializeComponent();
            this.DataModel = DataModel;
            treeView.ItemsSource = this.DataModel.GetDepartments();
        }

        /// <summary>
        /// Обрабатывает событие treeView_SelectedItemChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.DataModel.SetSelectDialog(e.NewValue);
            tbNewName.DataContext = this.DataModel.GetDepartmentName();
        }

        /// <summary>
        /// Нажата кнопка "ОК"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bOK_Click(object sender, RoutedEventArgs e)
        {
            //select.Name = tbNewName.Text;
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
    }
}