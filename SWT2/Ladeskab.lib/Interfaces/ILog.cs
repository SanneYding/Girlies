namespace Ladeskab.lib.Interfaces
{
    public interface ILog
    {
        void ReadLog();
        void LogDoorUnlocked(int rfid_ID);
        void LogDoorLocked(int rfid_ID);
    }
}