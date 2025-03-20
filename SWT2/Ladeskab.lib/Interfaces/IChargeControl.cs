namespace Ladeskab.lib.Interfaces;

public interface IChargeControl
{
    bool Connected { get; set; }
    double CurrentValue { get; }

    void StartCharge();
    void StopCharge();
    void SubscribeToEvent(IUsbCharger source);
}