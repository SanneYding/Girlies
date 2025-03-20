using Ladeskab.lib.Interfaces;

namespace Ladeskab.lib;

public class RfidReader : IRfidReader
{
    // Event triggered when an RFID is detected
    public event EventHandler<RfidEventArgs>? RfidChanged;

    public void OnRfidRead(int id)
    {
        // Validate the RFID (must be within a reasonable range)
        if (id is > 0 and < 999999999)
        {
            // Raise the event immediately without storing the RFID
            RfidChanged?.Invoke(this, new RfidEventArgs { id = id });
        }
    }
}
