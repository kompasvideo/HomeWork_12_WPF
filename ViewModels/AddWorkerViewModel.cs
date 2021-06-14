using DevExpress.Mvvm;
using HomeWork_WPF.Employees;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HomeWork_WPF.ViewModels
{
    class AddWorkerViewModel : ViewModelBase
    {
        /// <summary>
        /// Экземпляр сотрудника
        /// </summary>
        private Employee employee;
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string FirstName
        {
            get { return employee.FirstName; }
            set
            {
                employee.FirstName = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.FirstName)));
            }
        }
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string LastName
        {
            get { return employee.LastName; }
            set
            {
                employee.LastName = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.LastName)));
            }
        }
        /// <summary>
        /// Возраст сотрудника
        /// </summary>
        public int Age
        {
            get { return employee.Age; }
            set
            {
                employee.Age = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Age)));
            }
        }
        /// <summary>
        /// Должность сотрудника
        /// </summary>
        public string Job
        {
            get { return employee.Job; }
            set
            {
                employee.Job = value;
            }
        }
        /// <summary>
        /// Должность сотрудника
        /// </summary>
        public EnEmployee EEmployee
        {
            get { return employee.EEmployee; }
            set { employee.EEmployee = value; }
        }
        public Department SelectDepartment { get; set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="employee"></param>
        public AddWorkerViewModel(Employee employee, Department SelectDepartment)
        {
            this.employee = employee;
            this.SelectDepartment = SelectDepartment;
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
                    Employee l_employee;
                    foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                    {
                        if (window.Title == "Добавить сотрудника")
                        {
                            switch (Job)
                            {
                                case "Руководитель":
                                    l_employee = new Manager(FirstName, LastName, Age, SelectDepartment.DepartmentId);
                                    Messenger.Default.Send(l_employee);
                                    break;
                                case "Рабочий":
                                    l_employee = new Worker(FirstName, LastName, Age, SelectDepartment.DepartmentId);
                                    Messenger.Default.Send(l_employee);
                                    break;
                                case "Интерн":
                                    l_employee = new Intern(FirstName, LastName, Age, SelectDepartment.DepartmentId);
                                    Messenger.Default.Send(l_employee);
                                    break;
                            }
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
