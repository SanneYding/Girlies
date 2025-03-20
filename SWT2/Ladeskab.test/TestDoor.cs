using NUnit.Framework;
using System;
using Ladeskab.lib.Interfaces;

namespace Ladeskab.test
{
    public class TestDoor
    {
        private IDoor _uut;

        // Set up the test by initializing the Door object
        [SetUp]
        public void SetUp()
        {
            _uut = new Door();
        }

        // Test that the door is locked when it is closed
        [Test]
        public void door_Closed_LockIsLocked()
        {
            // Lock the door
            _uut.LockDoor();

            // Verify the door is locked
            Assert.That(_uut.isLocked, Is.True);
        }

        // Test that the door remains unlocked when it is open and locked
        [Test]
        public void door_Opened_LockIsLocked()
        {
            // Open the door
            _uut.OnDoorOpen();

            // Lock the door
            _uut.LockDoor();

            // Verify the door is not locked when open
            Assert.That(_uut.isLocked, Is.False);
        }

        // Test that the door remains unlocked when it is open
        [Test]
        public void door_Opened_LockIsUnlocked()
        {
            // Open the door
            _uut.OnDoorOpen();

            // Unlock the door
            _uut.UnlockDoor();

            // Verify the door is unlocked when open
            Assert.That(_uut.isLocked, Is.False);
        }

        // Test that the door is unlocked when it is closed
        [Test]
        public void door_Closed_LockIsUnlocked()
        {
            // Close the door
            _uut.OnDoorClose();

            // Unlock the door
            _uut.UnlockDoor();

            // Verify the door is unlocked when closed
            Assert.That(_uut.isLocked, Is.False);
        }

        // Test that the door close event is triggered when the door is opened
        [Test]
        public void door_Open_RaisesDoorClosedEvent()
        {
            // Open the door
            _uut.OnDoorOpen();

            // Event raised flag
            bool eventRaised = false;

            // Attach event handler to check when door is closed
            _uut.DoorEvent += (sender, args) => { eventRaised = !args.IsOpen; };

            // Close the door
            _uut.OnDoorClose();

            // Verify that the door closed event was raised
            Assert.That(eventRaised, Is.True);
        }

        // Test that the door open event is triggered when the door is closed
        [Test]
        public void door_Closed_RaisesDoorOpenedEvent()
        {
            // Close the door
            _uut.OnDoorClose();

            // Event raised flag
            bool eventRaised = false;

            // Attach event handler to check when door is opened
            _uut.DoorEvent += (sender, args) => { eventRaised = args.IsOpen; };

            // Open the door
            _uut.OnDoorOpen();

            // Verify that the door opened event was raised
            Assert.That(eventRaised, Is.True);
        }

        // Test that the door open event is not raised when the door is locked and closed
        [Test]
        public void door_ClosedLocked_NotRaisesDoorOpenedEvent()
        {
            // Close the door and lock it
            _uut.OnDoorClose();
            _uut.LockDoor();

            // Event raised flag
            bool eventRaised = false;

            // Attach event handler to check when door is opened
            _uut.DoorEvent += (sender, args) => { eventRaised = args.IsOpen; };

            // Attempt to open the door
            _uut.OnDoorOpen();

            // Verify that the door opened event was not raised because the door is locked
            Assert.That(eventRaised, Is.False);
        }
    }
}
