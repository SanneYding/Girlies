using System;
using System.Timers;
using Ladeskab.lib.Interfaces;

namespace UsbSimulator
{
    public class UsbChargerSimulator : IUsbCharger
    {
        // Constants defining max current, fully charged current, overload current, and time parameters
        // Maximum current the charger can supply, in milliamps (mA)
        private const double MaxCurrent = 500.0;

        // The current level when the device is fully charged, in milliamps (mA)
        private const double FullyChargedCurrent = 2.5;

        // The current level when an overload condition is simulated, in milliamps (mA)
        private const double OverloadCurrent = 750;

        // Total time (in minutes) that the charging process takes to complete
        private const int ChargeTimeMinutes = 20;

        // Interval (in milliseconds) between each current value update during charging
        private const int CurrentTickInterval = 250;


        // Event triggered when current value changes
        public event EventHandler<CurrentEventArgs>? CurrentValueEvent;

        // Properties for current value and connection status
        public double CurrentValue { get; private set; }
        public bool Connected { get; private set; }

        // Internal state variables for overload and charging status
        private bool _overload;
        private bool _charging;
        private System.Timers.Timer _timer;
        private int _ticksSinceStart;

        // Constructor initializes the simulator with default values
        public UsbChargerSimulator()
        {
            CurrentValue = 0.0;
            Connected = true;
            _overload = false;

            // Initialize the timer to update current values at regular intervals
            _timer = new System.Timers.Timer();
            _timer.Enabled = false;
            _timer.Interval = CurrentTickInterval;
            _timer.Elapsed += TimerOnElapsed;
        }

        // Method called on every timer tick to update the current value based on the charging state
        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            // Only update the current value if charging
            if (_charging)
            {
                _ticksSinceStart++;

                if (Connected && !_overload) // Normal charging
                {
                    // Calculate the new current value during charging
                    double newValue = MaxCurrent -
                                      _ticksSinceStart * (MaxCurrent - FullyChargedCurrent) / (ChargeTimeMinutes * 60 * 1000 / CurrentTickInterval);
                    CurrentValue = Math.Max(newValue, FullyChargedCurrent);
                }
                else if (Connected && _overload) // Overload condition
                {
                    CurrentValue = OverloadCurrent;
                }
                else if (!Connected) // Disconnected state
                {
                    CurrentValue = 0.0;
                }

                // Trigger event to notify about current value change
                OnNewCurrent();
            }
        }

        // Method to simulate connection or disconnection
        public void SimulateConnected(bool connected)
        {
            Connected = connected;
        }

        // Method to simulate overload condition
        public void SimulateOverload(bool overload)
        {
            _overload = overload;
        }

        // Method to start charging, initializes values and starts the timer
        public void StartCharge()
        {
            // Ignore if already charging
            if (!_charging)
            {
                // Set initial current based on connection and overload state
                if (Connected && !_overload)
                {
                    CurrentValue = 500;
                }
                else if (Connected && _overload)
                {
                    CurrentValue = OverloadCurrent;
                }
                else if (!Connected)
                {
                    CurrentValue = 0.0;
                }

                // Trigger event to notify about initial current value
                OnNewCurrent();
                _ticksSinceStart = 0;

                _charging = true;

                // Start the timer to update current value at intervals
                _timer.Start();
            }
        }

        // Method to stop charging, resets values and stops the timer
        public void StopCharge()
        {
            _timer.Stop();

            CurrentValue = 0.0;
            OnNewCurrent();

            _charging = false;
        }

        // Helper method to trigger the current value change event
        private void OnNewCurrent()
        {
            CurrentValueEvent?.Invoke(this, new CurrentEventArgs() { Current = this.CurrentValue });
        }
    }
}
