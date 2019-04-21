using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Class provided by instructor.
namespace FlightSimulator.ViewModel {
    public abstract class BaseNotify : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
