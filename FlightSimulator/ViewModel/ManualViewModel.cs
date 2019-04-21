using FlightSimulator.Model;
using System;

namespace FlightSimulator.ViewModel.Windows {
    // The class representing the manual view model.
    class ManualViewModel {
        // Creating the Model.
        private ManualModel model = new ManualModel();
        // Using the properties we will send the new values to the simulator.
        // Constructing a new string using the path and the value converted to a string.
        // The throttle controls.
        public double Throttle {
            set => model.SendCommand("set /controls/engines/current-engine/throttle " + Convert.ToString(value));
        }
        // The rudder controls.
        public double Rudder {
            set => model.SendCommand("set /controls/flight/rudder " + Convert.ToString(value));
        }
        // The elevator controls.
        public double Elevator {
            set => model.SendCommand("set /controls/flight/elevator " + Convert.ToString(value));
        }
        // The aileron controls.
        public double Aileron{
            set => model.SendCommand("set /controls/flight/aileron " + Convert.ToString(value));
        }
    }
}
