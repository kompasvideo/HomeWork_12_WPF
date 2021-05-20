using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeWork_WPF.Employees;


namespace HomeWork_WPF
{
    class SelectProvider : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Department select;        
        public Manager manager
        {
            get { return select.manager; }
            set
            {
                select.manager = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.manager)));
            }
        }
        public SelectProvider(Department p_select)
        {
            select = p_select;
        }
    }
}
