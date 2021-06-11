using DevExpress.Mvvm;
using HomeWork_WPF;
using HomeWork_WPF.Employees;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HomeWork_WPF.ViewModels
{
    class AddDepartmentViewModel : ViewModelBase
    {
        Department department { get; set; }
        /// <summary>
        /// Имя отдела
        /// </summary>
        public string Name
        {
            get { return department.Name; }
            set
            {
                department.Name = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.FirstName)));
            }
        }
        public Department SelectDepartment { get; set; }
        public AddDepartmentViewModel(Department department, Department SelectDepartment)
        {
            this.SelectDepartment = SelectDepartment;
            this.department = department;
        }
        /// <summary>
        /// Нажата кнопка "Ок"
        /// </summary>
        public ICommand bOK_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                    {
                        if (window.Title == "Добавить отдел")
                        {
                            Messenger.Default.Send(department);
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
                        if (window.Title == "Добавить отдел")
                        {
                            window.Close();
                        }
                    }
                });
            }
        }
    }
}
