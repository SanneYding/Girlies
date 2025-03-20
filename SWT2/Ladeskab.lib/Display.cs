using Ladeskab.lib.Interfaces;
using System;

namespace Ladeskab.lib
{
    public class Display : IDisplay
    {
        private IChargeControl _chargeControl; // Holds the charge control interface
        public string displayText { get; private set; } // Text to be displayed

        // Helper method to set display text and print it to the console
        private void UpdateDisplay(string message)
        {
            displayText = message;
            Console.WriteLine(message);
        }

        // Clears the display
        public void ClearDisplay()
        {
            UpdateDisplay(string.Empty);
        }

        // Display message to connect phone
        public void ConnectTel()
        {
            UpdateDisplay("User info: Connect phone.");
        }

        // Display message to load RFID
        public void LoadTheRFID()
        {
            UpdateDisplay("User info: Load RFID.");
        }

        // Display connection error message
        public void ConnectionError()
        {
            UpdateDisplay("User info: There is a connection error.");
        }

        // Display overload error message
        public void Overload()
        {
            UpdateDisplay("User info: CRITICAL OVERLOAD ERROR, STOPPING CHARGING!");
        }

        // Display occupied message
        public void Occupied()
        {
            UpdateDisplay("User info: Charging station occupied.");
        }

        // Display RFID error message
        public void RFIDerror()
        {
            UpdateDisplay("User info: RFID error");
        }

        // Display message to remove phone
        public void RemoveTlf()
        {
            UpdateDisplay("User info: Remove phone");
        }

        // Display message indicating the phone is charging
        public void IsCharging()
        {
            UpdateDisplay("Charging status: Phone is charging.");
        }

        // Display message indicating the phone is not charging
        public void IsNotCharging()
        {
            UpdateDisplay("Charging status: Phone is not charging.");
        }
    }
}
