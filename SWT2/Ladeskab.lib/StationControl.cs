using Ladeskab.lib.Interfaces;

namespace Ladeskab.lib;

public class StationControl
{
    // Enum representing the states of the charging station
    private enum LadeskabState
    {
        Available,    // Station is available for use
        Locked,       // Station is locked
        DoorOpen      // Door is open
    }

    // Member variables
    private LadeskabState _state;
    private IRfidReader _reader;
    private readonly IChargeControl _charger;
    private readonly IDoor _door;
    private readonly IDisplay _display;
    private readonly ILog _log;
    private int _oldId; // Stores the previous RFID ID for validation

    // Constructor to initialize the station control with necessary components
    public StationControl(IChargeControl charger, IDoor door, IRfidReader reader, IDisplay display, ILog logger)
    {
        _state = 0; // Initial state set to 'Available'
        _log = logger;
        _charger = charger;
        _door = door;
        _display = display;
        _reader = reader;
        SubscribeToRfid(reader);  // Subscribe to RFID reader events
        SubscribeDoorEvent(door); // Subscribe to door events
    }

    // Subscribes to RFID reader events (RFID detected)
    public void SubscribeToRfid(IRfidReader reader)
    {
        reader.RfidChanged += OnRFIDEventReceived; // Trigger the RFID event handler when RFID is detected
    }

    // Subscribes to door events (door open/close)
    public void SubscribeDoorEvent(IDoor door)
    {
        door.DoorEvent += OnDoorEventReceived; // Trigger the door event handler when door status changes
    }

    // Handles the logic when an RFID is detected, based on the current station state
    private void RfidDetected(int id)
    {
        switch (_state)
        {
            case LadeskabState.Available:
                // If the station is available and charger is connected, lock the door and start charging
                if (_charger.Connected)
                {
                    _door.LockDoor();          
                    _charger.StartCharge();    
                    _display.Occupied();       
                    _oldId = id;               
                    _log.LogDoorLocked(_oldId);
                    _state = LadeskabState.Locked; 
                }
                else
                {
                    _display.ConnectionError(); 
                }
                break;

            case LadeskabState.DoorOpen:
                // No specific actions needed when door is open
                break;

            case LadeskabState.Locked:
                // If the door is locked, verify the RFID ID to stop charging and unlock the door
                if (id == _oldId)
                {
                    _charger.StopCharge();
                    _door.UnlockDoor();
                    _display.RemoveTlf(); 
                    _log.LogDoorUnlocked(_oldId);
                    _state = LadeskabState.Available;
                }
                else
                {
                    _display.RFIDerror(); // Show error if the RFID ID doesn't match
                }
                break;
        }
    }

    // Handles door events (open/close)
    private void OnDoorEventReceived(object? sender, DoorEventArgs door)
    {
        if (door.IsOpen)
        {
            _display.ConnectTel();   // Show message to connect the phone when the door is open
            _state = LadeskabState.DoorOpen; // Change the state to 'DoorOpen'
        }
        else
        {
            _display.ClearDisplay(); // Clear the display when the door is closed
            _state = LadeskabState.Available; // Change the state to 'Available'
        }
    }

    // Handles RFID event received
    private void OnRFIDEventReceived(object? sender, RfidEventArgs e)
    {
        RfidDetected(e.id); // Trigger the RFID detection logic with the detected ID
    }
}
