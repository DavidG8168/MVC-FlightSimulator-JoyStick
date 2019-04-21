using FlightSimulator.Models.Interface;

// Class provided by instructor.
namespace FlightSimulator.Models {
    public class ApplicationSettingsModel : ISettingsModel {
        #region Singleton
        private static ISettingsModel m_Instance = null;
        // Allow only one instance of the class.
        public static ISettingsModel Instance {
            get {
                if(m_Instance == null) {
                    m_Instance = new ApplicationSettingsModel();
                }
                return m_Instance;
            }
        }
        #endregion
        // Property for the server IP.
        public string FlightServerIP {
            get { return Properties.Settings.Default.FlightServerIP; }
            set { Properties.Settings.Default.FlightServerIP = value; }
        }
        // Property for the command port.
        public int FlightCommandPort {
            get { return Properties.Settings.Default.FlightCommandPort; }
            set { Properties.Settings.Default.FlightCommandPort = value; }
        }
        // Property for the information port.
        public int FlightInfoPort {
            get { return Properties.Settings.Default.FlightInfoPort; }
            set { Properties.Settings.Default.FlightInfoPort = value; }
        }
        // Save the settings.
        public void SaveSettings() {
            Properties.Settings.Default.Save();
        }
        // Reload the settings.
        public void ReloadSettings() {
            Properties.Settings.Default.Reload();
        }
    }
}
