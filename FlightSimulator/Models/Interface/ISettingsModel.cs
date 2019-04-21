// Class provided by instructor.
namespace FlightSimulator.Models.Interface {
    public interface ISettingsModel {
        // The IP Of the Flight Server
        string FlightServerIP { get; set; }
        // The Port of the Flight Server
        int FlightInfoPort { get; set; }
        // The Port of the Flight Server
        int FlightCommandPort { get; set; }        
        // Save the settings.
        void SaveSettings();
        // Set the settings.
        void ReloadSettings();
    }
}
