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

        private Manager managerP;
        public Manager manager
        {
            get { return managerP; }
            set
            {
                managerP = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.manager)));
            }
        }
        public SelectProvider(Manager manager)
        {
            this.manager = manager;
        }
    }
}
