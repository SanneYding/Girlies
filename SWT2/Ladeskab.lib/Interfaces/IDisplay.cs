namespace Ladeskab.lib.Interfaces
{
    public interface IDisplay
    {
        public string displayText { get; }
        void ClearDisplay();
        void RemoveTlf();
        void LoadTheRFID();
        void Occupied();
        void RFIDerror();
        void ConnectionError();
        void ConnectTel();
        void IsCharging();
        void IsNotCharging();
        void Overload();
    }
}