using Ladeskab.lib.Interfaces;

namespace Ladeskab.lib;

public class ChargeControl : IChargeControl
{
    private readonly IUsbCharger _usbCharger; // The USB charger that controls charging
    private readonly IDisplay _display; // The display used to show status messages

    public bool Connected { get; set; } // Indicates whether the charger is connected
    public double CurrentValue { get; private set; } // The current value received from the USB charge

    public void SubscribeToEvent(IUsbCharger source)
    {
        source.CurrentValueEvent += OnCurrentValueEventReceived; // Event handler for current value updates
    }

    // Constructor to initialize the charge control with a USB charger and display
    public ChargeControl(IUsbCharger usbCharger, IDisplay display)
    {
        _usbCharger = usbCharger;
        _display = display;
        Connected = _usbCharger.Connected;
    }

    // Starts the charging process depending on the current value and connection status
    public void StartCharge()
    {
        Connected = _usbCharger.Connected;
        if (!Connected)
        {
            _display.ConnectionError();
            return;
        }

        // Handle overload case
        if (CurrentValue > 1000)
        {
            StopCharge();  // Stop charging
            _display.Overload();  // Show overload message
        }
        // Handle normal charging case
        else if (CurrentValue > 5 && CurrentValue <= 1000)
        {
            _usbCharger.StartCharge();
            _display.IsCharging();
        }
        // Handle low current case
        else if (CurrentValue > 0 && CurrentValue <= 5)
        {
            StopCharge();  // Stop charging if the current is too low
        }
        // Handle case where current is zero or negative, start charging again
        else if (CurrentValue <= 0)
        {
            _usbCharger.StartCharge();
            _display.IsCharging();
        }
    }

    // Stops the charging process and updates the display
    public void StopCharge()
    {
        _usbCharger.StopCharge();
        _display.IsNotCharging();
    }

    // Event handler for when the current value changes, which starts or stops charging
    private void OnCurrentValueEventReceived(object sender, CurrentEventArgs e)
    {
        CurrentValue = e.Current;

        StartCharge();
    }
}