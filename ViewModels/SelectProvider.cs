using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using HomeWork_WPF.Employees;


namespace HomeWork_WPF
{
    class SelectProvider : ViewModelBase
    {
        public Manager manager { get; set; }
        public SelectProvider(Manager manager)
        {
            this.manager = manager;
        }
    }
}
