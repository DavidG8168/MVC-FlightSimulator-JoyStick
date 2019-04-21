using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Class provided by instructor.
namespace FlightSimulator.Models.Interface {
    public interface ISettingsModel {
        // The IP Of the Flight Server
        string FlightServerIP { get; set; }
        // The Port of the Flight Server
        int FlightInfoPort { get; set; }
        // The Port of the Flight Server
        int FlightCommandPort { get; set; }          
        void SaveSettings();
        void ReloadSettings();
    }
}
