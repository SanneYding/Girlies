namespace Ladeskab.lib.Interfaces;

public class Door : IDoor
{
    // Property to track if the door is open
    public bool isOpen { get; private set; }

    // Property to track if the door is locked
    public bool isLocked { get; private set; }

    // Event to notify listeners about door status changes
    public event EventHandler<DoorEventArgs>? DoorEvent;

    // Lock the door if it is not open
    public void LockDoor()
    {
        if (!isOpen)
            isLocked = true;  // Lock the door if it's not already open
    }

    // Unlock the door
    public void UnlockDoor()
    {
        isLocked = false;  // Unlock the door
    }

    // Open the door if it's not locked and not already open
    public void OnDoorOpen()
    {
        if (!isLocked && !isOpen)
        {
            isOpen = true; // Mark the door as open
            OnDoorEvent(new DoorEventArgs { IsOpen = isOpen }); // Notify about the door state change
        }
    }

    // Close the door if it's currently open
    public void OnDoorClose()
    {
        if (isOpen)
        {
            isOpen = false; // Mark the door as closed
            OnDoorEvent(new DoorEventArgs { IsOpen = isOpen }); // Notify about the door state change
        }
    }

    // Raise the DoorEvent to notify subscribers of the door's state
    protected virtual void OnDoorEvent(DoorEventArgs isOpen)
    {
        DoorEvent?.Invoke(this, isOpen);  // Trigger the event if there are listeners
    }
}
