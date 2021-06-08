using DevExpress.Mvvm;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeWork_WPF.ViewModels
{
    class AddWorkerViewModel : ViewModelBase
    {
        public ICommand bOK_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {

                    foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                    {
                        if (window.Title == "Добавить сотрудника")
                        {
                            window.Close();
                        }
                    }
                });
            }
        }
        /// <summary>
        /// Нажата кнопка "Отмена"
        /// </summary>
        public ICommand bCancel_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                    {
                        if (window.Title == "Добавить сотрудника")
                        {
                            window.Close();
                        }
                    }
                });
            }
        }
    }
}
