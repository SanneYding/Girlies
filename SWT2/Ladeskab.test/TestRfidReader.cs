using Ladeskab.lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.lib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ladeskab.test
{
    public class TestRfidReader
    {
        private IRfidReader _uut;

        // Set up the test environment, initializing the RFID reader
        [SetUp]
        public void SetUp()
        {
            _uut = new RfidReader();
        }

        // Test case to ensure the correct event is raised when a valid RFID is scanned
        [Test]
        public void rfidReader_ScannedCorrectEventRaised()
        {
            int eventRaised = 0;
            _uut.RfidChanged += (sender, args) => { eventRaised = args.id; }; // Subscribe to the RfidChanged event

            _uut.OnRfidRead(007); // Simulate scanning a valid RFID

            Assert.That(eventRaised, Is.EqualTo(007)); // Assert that the event ID matches the expected value
        }

        // Test case to ensure no event is raised when an illegal RFID ID is scanned
        [Test]
        public void rfidReader_ScannedIllegalIDEventNotRaised()
        {
            int eventRaised = -1;
            _uut.RfidChanged += (sender, args) => { eventRaised = args.id; };

            _uut.OnRfidRead(-7);

            Assert.That(eventRaised, Is.EqualTo(-1));
        }

        // Test case to ensure that no exception is thrown when RfidChanged is null
        [Test]
        public void rfidReader_RfidChangedIsNull_NoExceptionThrown()
        {
            _uut.RfidChanged += null;
            
            Assert.DoesNotThrow(() => _uut.OnRfidRead(123));
        }

    }
}
