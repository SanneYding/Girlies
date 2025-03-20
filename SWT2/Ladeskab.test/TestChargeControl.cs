using NUnit.Framework;
using NSubstitute;
using Ladeskab.lib.Interfaces;
using UsbSimulator;
using Ladeskab.lib;

namespace Ladeskab.test
{
    public class TestChargeControl
    {
        private ChargeControl _uut;
        private IUsbCharger _usbChargerMock;
        private IDisplay _displayMock;

        [SetUp]
        public void SetUp()
        {
            // Initialize mocks and system under test (SUT)
            _usbChargerMock = Substitute.For<IUsbCharger>();
            _displayMock = Substitute.For<IDisplay>();
            _uut = new ChargeControl(_usbChargerMock, _displayMock);
        }


        // Test that current value is correctly updated when event is triggered
        [TestCase(50)]
        public void StartCharge_WhenCurrentValueChangesEvent(double Current)
        {
            _uut.SubscribeToEvent(_usbChargerMock);

            // Trigger event with the specified current value
            _usbChargerMock.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = Current });

            // Check if the CurrentValue was updated correctly
            Assert.That(Current, Is.EqualTo(_uut.CurrentValue));
        }

        // Test when current value is below valid range, charge should stop
        [TestCase(0.01)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(5)]
        public void StartCharge_WhenCurrentValueBelowValidRange_StopsCharge(double outOfCurrentValue)
        {
            _usbChargerMock.Connected.Returns(true);
            _uut.SubscribeToEvent(_usbChargerMock);

            _usbChargerMock.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = outOfCurrentValue });

            _usbChargerMock.Received(1).StopCharge();
            _usbChargerMock.DidNotReceive().StartCharge();
        }

        // Test when current value exceeds valid range (overload), charge should stop
        [TestCase(1000.01)] 
        [TestCase(1500.00)]  
        [TestCase(2000.00)]
        public void StartCharge_WhenCurrentValueAboveValidRange_StopsCharge(Double overloadCurrentValue)
        {
            _usbChargerMock.Connected.Returns(true);
            _uut.SubscribeToEvent(_usbChargerMock);

            _usbChargerMock.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = overloadCurrentValue });

            _usbChargerMock.Received(1).StopCharge();
            _usbChargerMock.DidNotReceive().StartCharge();
        }

        // Test when current value is within valid range, charging should start
        [TestCase(5.01)]
        [TestCase(100.00)]
        [TestCase(400.0)]
        [TestCase(500.00)]
        [TestCase(999.99)]
        [TestCase(1000.00)]
        public void StartCharge_WhenEventRaisedValueValidRange_StartCharge(double validCurrentValue)
        {
            _usbChargerMock.Connected.Returns(true); 
            _uut.SubscribeToEvent(_usbChargerMock);

            _usbChargerMock.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = validCurrentValue });

            _usbChargerMock.Received(1).StartCharge();
            _usbChargerMock.DidNotReceive().StopCharge();
        }

        // Test when current value is zero or negative, charge should start
        [TestCase(0)]
        [TestCase(-0.01)]
        [TestCase(-1.00)]
        [TestCase(-10.00)]
        public void StopCharge_WhenEventRaisedValueZero_StartCharge(double zeroCurrentValue)
        {
            _usbChargerMock.Connected.Returns(true);
            _uut.SubscribeToEvent(_usbChargerMock);

            _usbChargerMock.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = zeroCurrentValue });

            _usbChargerMock.Received(1).StartCharge();
            _usbChargerMock.DidNotReceive().StopCharge();
        }

        // Test that StopCharge correctly stops the charging process
        [Test]
        public void StopCharge_StopsCharge()
        {
            _uut.StopCharge();

            _usbChargerMock.Received(1).StopCharge();
            _usbChargerMock.DidNotReceive().StartCharge();

        }

    }
}
