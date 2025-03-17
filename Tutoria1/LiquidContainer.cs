namespace Tutoria1;

public class LiquidContainer : Container, IHazardousNotifier
{

    public LiquidContainer(double height, double tareWeight, double depth, double maxPayload) :
        base(height, tareWeight, depth, maxPayload) {}

    public void SendNotificationAboutHazardousOccurance(string message)
    {
        Console.WriteLine($"{GetSerialNumber()} (Hazardous Event Caught!) : {message}");
    }

    protected override string GetContainerType()
    {
        return "L";
    }
    
    // Overload the function as boolean is needed
    public void LoadCargo(double mass, bool hazardous = false)
    {
        CargoMass += mass;
        
        if (CargoMass > MaxPayload)
        {
            throw new OverfillException($"Cannot load {mass} kg. Maximum payload is {MaxPayload} kg.");
        }

        if (CargoMass > 0.9 * MaxPayload)
        {
            SendNotificationAboutHazardousOccurance("Loading liquid cargo, which is greater then 90% of the max payload is dangerous.");
        }

        if (hazardous && CargoMass > 0.5 * MaxPayload)
        {
            SendNotificationAboutHazardousOccurance("Loading hazardous cargo, which is greater then 50% of the max payload is dangerous.");
        }
    }
    
}