using Ladeskab.lib.Interfaces;
using Ladeskab.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.test
{
    internal class TestDisplay
    {
        private IDisplay _uut;

        // Set up the test by initializing the Display object
        [SetUp]
        public void SetUp()
        {
            _uut = new Display();
        }

        // Test that the display is cleared and shows an empty string
        [Test]
        public void display_clear_emptyString()
        {
            // Ensure display text is not empty before clearing
            _uut.ConnectTel();
            Assert.That(_uut.displayText, Is.Not.EqualTo(""));

            // Clear the display
            _uut.ClearDisplay();

            // Verify the display text is now empty
            Assert.That(_uut.displayText, Is.EqualTo(""));  
        }

        // Test that the correct message is shown when connecting a phone
        [Test]
        public void display_connectTel_messageShown()
        {
            _uut.ClearDisplay();

            _uut.ConnectTel();

            Assert.That(_uut.displayText, Is.EqualTo("User info: Connect phone."));
        }


        // Test that the correct message is shown when RFID is loaded
        [Test]
        public void display_LoadTheRFID_messageShown()
        {
            _uut.ClearDisplay();

            _uut.LoadTheRFID();

            Assert.That(_uut.displayText, Is.EqualTo("User info: Load RFID."));
        }

        // Test that the correct message is shown when there's a connection error
        [Test]
        public void display_connectionerror_messageShown()
        {
            _uut.ClearDisplay();

            _uut.ConnectionError();

            Assert.That(_uut.displayText, Is.EqualTo("User info: There is a connection error."));
        }

        // Test that the correct message is shown when the locker is occupied
        [Test]
        public void display_occupied_messageShown()
        {
            _uut.ClearDisplay();

            _uut.Occupied();

            Assert.That(_uut.displayText, Is.EqualTo("User info: Charging station occupied."));
        }

        // Test that the correct message is shown when there's an RFID error
        [Test]
        public void display_RFIDerror_messageShown()
        {
            _uut.ClearDisplay();

            _uut.RFIDerror();

            Assert.That(_uut.displayText, Is.EqualTo("User info: RFID error"));
        }

        // Test that the correct message is shown when the phone is removed
        [Test]
        public void display_RemoveTlf_messageShown()
        {
            _uut.ClearDisplay();

            _uut.RemoveTlf();

            Assert.That(_uut.displayText, Is.EqualTo("User info: Remove phone"));
        }

        // Test that the correct message is shown when the phone is charging
        [Test]
        public void display_isCharging_messageShown()
        {
            _uut.ClearDisplay();

            _uut.IsCharging();

            Assert.That(_uut.displayText, Is.EqualTo("Charging status: Phone is charging."));
        }

        // Test that the correct message is shown when the phone is not charging
        [Test]
        public void display_isNotCharging_messageShown()
        {
            _uut.ClearDisplay();

            _uut.IsNotCharging();

            Assert.That(_uut.displayText, Is.EqualTo("Charging status: Phone is not charging."));
        }

        // Test that the correct message is shown when there is an overload error
        [Test]
        public void display_overload_messageShown()
        {
            _uut.ClearDisplay();

            _uut.Overload();

            Assert.That(_uut.displayText, Is.EqualTo("User info: CRITICAL OVERLOAD ERROR, STOPPING CHARGING!"));
        }
    }
}
