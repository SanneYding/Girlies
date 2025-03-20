
namespace Ladeskab.lib.Interfaces
{
    public class RfidEventArgs : EventArgs
    {
        public int id { get; set; }
    }
    public interface IRfidReader
    {
        public event EventHandler<RfidEventArgs> RfidChanged;
        void OnRfidRead(int id);
    }
}