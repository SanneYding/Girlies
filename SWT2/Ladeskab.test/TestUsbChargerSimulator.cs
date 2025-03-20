using NUnit.Framework;
using System;
using System.Threading;

namespace UsbSimulator.Test
{
    [TestFixture]
    public class TestUsbChargerSimulator
    {
        private UsbChargerSimulator _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new UsbChargerSimulator(); // Initialize the UsbChargerSimulator before each test
        }

        [Test]
        public void ctor_IsConnected()
        {
            // Test if the charger is connected by default
            Assert.That(_uut.Connected, Is.True);
        }

        [Test]
        public void ctor_CurentValueIsZero()
        {
            // Test if the charger’s current value is initially set to zero
            Assert.That(_uut.CurrentValue, Is.Zero);
        }

        [Test]
        public void SimulateDisconnected_ReturnsDisconnected()
        {
            // Test if setting the charger to disconnected updates the connection status
            _uut.SimulateConnected(false);
            Assert.That(_uut.Connected, Is.False);
        }

        [Test]
        public void Started_WaitSomeTime_ReceivedSeveralValues()
        {
            // Test if several current values are received during the charging process
            int numValues = 0;
            _uut.CurrentValueEvent += (o, args) => numValues++;

            _uut.StartCharge();
            Thread.Sleep(1100);

            Assert.That(numValues, Is.GreaterThan(4));
        }

        [Test]
        public void Started_WaitSomeTime_ReceivedChangedValue()
        {
            // Test if the current value changes during charging
            double lastValue = 1000;
            _uut.CurrentValueEvent += (o, args) => lastValue = args.Current;

            _uut.StartCharge();
            Thread.Sleep(300);

            Assert.That(lastValue, Is.LessThan(500.0));
        }

        [Test]
        public void StartedNoEventReceiver_WaitSomeTime_PropertyChangedValue()
        {
            // Test if the current value changes when charging without event receiver
            _uut.StartCharge();
            Thread.Sleep(300);

            Assert.That(_uut.CurrentValue, Is.LessThan(500.0));
        }

        [Test]
        public void Started_WaitSomeTime_PropertyMatchesReceivedValue()
        {
            // Test if the last current value received matches the current property value
            double lastValue = 1000;
            _uut.CurrentValueEvent += (o, args) => lastValue = args.Current;

            _uut.StartCharge();
            Thread.Sleep(1100);

            Assert.That(lastValue, Is.EqualTo(_uut.CurrentValue));
        }

        [Test]
        public void Started_SimulateOverload_ReceivesHighValue()
        {
            // Test if overload condition results in a high current value
            ManualResetEvent pause = new ManualResetEvent(false);
            double lastValue = 0;

            _uut.CurrentValueEvent += (o, args) =>
            {
                lastValue = args.Current;
                pause.Set(); // Signal that the value has been received
            };

            _uut.StartCharge();
            _uut.SimulateOverload(true); // Simulate overload
            pause.Reset();

            pause.WaitOne(300); // Wait for the overload value to be received
            Assert.That(lastValue, Is.GreaterThan(500.0)); // Expect a high value due to overload
        }

        [Test]
        public void Started_SimulateDisconnected_ReceivesZero()
        {
            // Test if the charger receives zero current when disconnected
            ManualResetEvent pause = new ManualResetEvent(false);
            double lastValue = 1000;

            _uut.CurrentValueEvent += (o, args) =>
            {
                lastValue = args.Current;
                pause.Set();
            };

            _uut.StartCharge();
            _uut.SimulateConnected(false); // Simulate disconnection
            pause.Reset();

            pause.WaitOne(300); // Wait for the zero value to be received
            Assert.That(lastValue, Is.Zero); // Expect zero current when disconnected
        }

        [Test]
        public void SimulateOverload_Start_ReceivesHighValueImmediately()
        {
            // Test if the charger immediately sends a high value when overload is simulated
            double lastValue = 0;

            _uut.CurrentValueEvent += (o, args) =>
            {
                lastValue = args.Current;
            };

            _uut.SimulateOverload(true); // Simulate overload
            _uut.StartCharge();

            Assert.That(lastValue, Is.GreaterThan(500.0)); // Expect high value immediately on start
        }

        [Test]
        public void SimulateDisconnected_Start_ReceivesZeroValueImmediately()
        {
            // Test if the charger immediately sends zero current when disconnected on start
            double lastValue = 1000;

            _uut.CurrentValueEvent += (o, args) =>
            {
                lastValue = args.Current;
            };

            _uut.SimulateConnected(false); // Simulate disconnection
            _uut.StartCharge();

            Assert.That(lastValue, Is.Zero); // Expect zero value immediately when disconnected
        }

        [Test]
        public void StopCharge_IsCharging_ReceivesZeroValue()
        {
            // Test if the current value is zero after stopping the charge
            double lastValue = 1000;
            _uut.CurrentValueEvent += (o, args) => lastValue = args.Current;

            _uut.StartCharge();
            Thread.Sleep(300); // Allow time for charging

            _uut.StopCharge(); // Stop charging

            Assert.That(lastValue, Is.EqualTo(0.0)); // Expect zero current after stopping charge
        }

        [Test]
        public void StopCharge_IsCharging_PropertyIsZero()
        {
            // Test if the current property value is zero after stopping the charge
            _uut.StartCharge();
            Thread.Sleep(300);

            _uut.StopCharge();

            Assert.That(_uut.CurrentValue, Is.EqualTo(0.0)); // Expect property to be zero after stopping charge
        }

        [Test]
        public void StopCharge_IsCharging_ReceivesNoMoreValues()
        {
            // Test if no more current values are received after stopping the charge
            double lastValue = 1000;
            _uut.CurrentValueEvent += (o, args) => lastValue = args.Current;

            _uut.StartCharge();
            Thread.Sleep(300); // Allow time for charging

            _uut.StopCharge(); // Stop charging
            lastValue = 1000;

            Thread.Sleep(300); // Wait for the next tick

            Assert.That(lastValue, Is.EqualTo(1000.0)); // Expect no new value after stopping charge
        }
    }
}
