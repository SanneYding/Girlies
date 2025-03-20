using Ladeskab.lib.Interfaces;
using Ladeskab.lib;
using System;
using NSubstitute;

namespace Ladeskab.test
{
    internal class TestStationControl
    {
        private StationControl _uut;
        private IChargeControl _chargeControlMock;
        private ILog _logMock;
        private IDoor _doorMock;
        private IDisplay _displayMock;
        private RfidReader _rfidReaderMock;

        // Set up mock dependencies and the StationControl object before each test
        [SetUp]
        public void Setup()
        {
            _chargeControlMock = Substitute.For<IChargeControl>();
            _doorMock = Substitute.For<IDoor>();
            _displayMock = Substitute.For<IDisplay>();
            _rfidReaderMock = Substitute.For<RfidReader>();
            _logMock = Substitute.For<ILog>();
            _uut = new StationControl(_chargeControlMock, _doorMock, _rfidReaderMock, _displayMock, _logMock);
        }

        // Helper method for testing RFID reading with different cases
        private void TestRfidRead(int testRfid, bool expectedStartCharge, bool expectedLockDoor, bool expectedOccupied, bool expectedConnectionError, bool expectedRFIDError)
        {
            _chargeControlMock.Connected = true;

            _rfidReaderMock.OnRfidRead(testRfid);

            if (expectedStartCharge)
                _chargeControlMock.Received(1).StartCharge();

            if (expectedLockDoor)
                _doorMock.Received(1).LockDoor();

            if (expectedOccupied)
                _displayMock.Received(1).Occupied();

            if (expectedConnectionError)
                _displayMock.Received(1).ConnectionError();

            if (expectedRFIDError)
                _displayMock.Received(1).RFIDerror();
        }

        // Test subscribing to the RfidChanged event.
        [Test]
        public void Subscribe_SubscribesToRfidChangedEvent()
        {
            _uut.SubscribeToRfid(_rfidReaderMock);
            _rfidReaderMock.Received(1).RfidChanged += Arg.Any<EventHandler<RfidEventArgs>>();
        }

        // Parametrized tests with valid RFID values
        [TestCase(1, true, true, true, false, false)]
        [TestCase(2, true, true, true, false, false)]
        [TestCase(10, true, true, true, false, false)]
        [TestCase(999999997, true, true, true, false, false)]
        [TestCase(999999998, true, true, true, false, false)]
        public void OnRfidRead_ValidRfid_Test(int testRfid, bool expectedStartCharge, bool expectedLockDoor, bool expectedOccupied, bool expectedConnectionError, bool expectedRFIDError)
        {
            TestRfidRead(testRfid, expectedStartCharge, expectedLockDoor, expectedOccupied, expectedConnectionError, expectedRFIDError);
        }

        // Parametrized tests with invalid RFID values
        [TestCase(-1, false, false, false, false, false)]
        [TestCase(0, false, false, false, false, false)]
        [TestCase(999999999, false, false, false, false, false)]
        [TestCase(1000000000, false, false, false, false, false)]
        public void OnRfidRead_InvalidRfid_Test(int testRfid, bool expectedStartCharge, bool expectedLockDoor, bool expectedOccupied, bool expectedConnectionError, bool expectedRFIDError)
        {
            TestRfidRead(testRfid, expectedStartCharge, expectedLockDoor, expectedOccupied, expectedConnectionError, expectedRFIDError);
        }

        // Test the case where the door is open and no actions should be taken
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(999999997)]
        [TestCase(999999998)]
        public void RfidDetected_DoorOpen_NoActionTaken(int testRfid)
        {
            _chargeControlMock.Connected.Returns(true);

            _doorMock.DoorEvent += Raise.EventWith(new DoorEventArgs() { IsOpen = true });
            _rfidReaderMock.OnRfidRead(testRfid);

            _chargeControlMock.DidNotReceive().StartCharge();
            _doorMock.DidNotReceive().LockDoor();
            _displayMock.DidNotReceive().Occupied();
            _logMock.DidNotReceive().LogDoorLocked(Arg.Any<int>());
        }

        // Test that the display is cleared when the door is closed
        [Test]
        public void OnDoorEventReceived_DoorClosed_StateChangedToAvailable()
        {
            _doorMock.DoorEvent += Raise.EventWith(new DoorEventArgs() { IsOpen = false });
            _displayMock.Received(1).ClearDisplay();
        }

        // Test that the display shows "Connect Telephone" when the door opens
        [Test]
        public void OnDoorEventReceived_DoorOpen_StateChangedToAvailable()
        {
            _doorMock.DoorEvent += Raise.EventWith(new DoorEventArgs() { IsOpen = true });
            _displayMock.Received(1).ConnectTel();
        }
    }
}
