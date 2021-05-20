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
    /// Логика взаимодействия для AddDepartmentWindow.xaml
    /// </summary>
    public partial class AddDepartmentWindow : Window
    {
        // Список отделов
        ObservableCollection<Department> departments;

        // выбранный TreeViewItem 
        Department select;

        /// <summary>
        /// DepartmentId
        /// </summary>
        static uint i;

        public AddDepartmentWindow(ObservableCollection<Department> departments)
        {
            InitializeComponent();
            this.departments = departments;
            treeView.ItemsSource = departments;
            //DepartmentClass tvi_main = (DepartmentClass)treeView.Items.CurrentItem;
        }

        /// <summary>
        /// Нажата кнопка "ОК"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bOK_Click(object sender, RoutedEventArgs e)
        {
            i = 0;
            select.Departments.Add(new Department(tbNewName.Text,
                GetNextDepartmentId(departments) + 1, new ObservableCollection<Department>()));
            this.Close();
        }

        /// <summary>
        /// Получает наибольший DepartmentId
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        private uint GetNextDepartmentId(ObservableCollection<Department> departments)
        {
            foreach (var dep in departments)
            {
                if (dep.DepartmentId > i) i = dep.DepartmentId;
                if (dep.Departments.Count > 0) GetNextDepartmentId(dep.Departments);
            }
            return i;
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