using FlightSimulator.Models;
using System;

namespace FlightSimulator.ViewModels.Windows {
    // The class representing the manual view model.
    class ManualViewModel {
        // Creating the Model.
        private ManualModel model = new ManualModel();
        // Using the properties we will send the new values to the simulator.
        // Constructing a new string using the path and the value converted to a string.
        // The throttle controls.
        public double Throttle {
            set => model.SendCommandToSimulator("set /controls/engines/current-engine/throttle " + Convert.ToString(value));
        }
        // The rudder controls.
        public double Rudder {
            set => model.SendCommandToSimulator("set /controls/flight/rudder " + Convert.ToString(value));
        }
        // The elevator controls.
        public double Elevator {
            set => model.SendCommandToSimulator("set /controls/flight/elevator " + Convert.ToString(value));
        }
        // The aileron controls.
        public double Aileron{
            set => model.SendCommandToSimulator("set /controls/flight/aileron " + Convert.ToString(value));
        }
    }
}
