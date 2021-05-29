using DevExpress.Mvvm;
using HomeWork_WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeWork_WPF.ViewModels
{
    class MainViewModel : ViewModelBase
    {        
        public int Clicks { get; set; }
        public ICommand AddWorker_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    AddWorkerWindow addWorkerWindow = new AddWorkerWindow();
                    if (addWorkerWindow.ShowDialog() == true)
                    {
                        //Model.repository.Employees.Add(this.DataModel.GetNewEmployee());
                        //Salary.DataContext = this.DataModel.GetSelect();
                    }
                });
            }
        }
    }
}
