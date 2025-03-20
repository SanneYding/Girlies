using static Ladeskab.lib.Interfaces.Door;

namespace Ladeskab.lib.Interfaces
{
    public interface IDoor
    {
        bool isOpen { get; } 
        bool isLocked { get; }
        void LockDoor();
        void UnlockDoor();

        void OnDoorOpen();
        void OnDoorClose();

        event EventHandler<DoorEventArgs>? DoorEvent;
    }
    public class DoorEventArgs : EventArgs
    {
        public bool IsOpen { get; set; }
    }
}