using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_WPF
{
    class DepartmentNameProvider : ViewModelBase
    {       
        public string Name { get; set; }
        public DepartmentNameProvider(string name)
        {
            this.Name = name;
        }
    }
}
