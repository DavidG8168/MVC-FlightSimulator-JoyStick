using System.ComponentModel;

namespace FlightSimulator.ViewModels {
    // Class provided by instructor.
    public abstract class BaseNotify : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
