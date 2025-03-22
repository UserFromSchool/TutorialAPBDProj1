using System.ComponentModel;

namespace Tutoria1;

public class LiquidContainer : Container, IHazardousNotifier
{

    public bool IsHazardous { get; private set; }

    public LiquidContainer(double height, double tareWeight, double depth, double maxPayload) :
        base(height, tareWeight, depth, maxPayload)
    {
        IsHazardous = false;
    }

    public void SendNotificationAboutHazardousOccurance(string message)
    {
        Console.WriteLine($"{GetSerialNumber()} (Hazardous Event Caught!) : {message}");
    }

    protected override string GetContainerType()
    {
        return "L";
    }

    public override void LoadCargo(double mass)
    {
        LoadCargo(mass, false);
    }
    
    public void LoadCargo(double mass, bool hazardous)
    {
        if (IsHazardous != hazardous && CargoMass != 0)
        {
            throw new Exception($"Liquid container contains a hazardous({IsHazardous}) liquid, while trying to load hazardous({hazardous}) one!");
        }
        base.LoadCargo(mass);
        IsHazardous = hazardous;
        if (CargoMass > 0.9 * MaxPayload)
        {
            SendNotificationAboutHazardousOccurance("Loading liquid cargo, which is greater then 90% of the max payload is dangerous.");
        }
        if (hazardous && CargoMass > 0.5 * MaxPayload)
        {
            SendNotificationAboutHazardousOccurance("Loading hazardous cargo, which is greater then 50% of the max payload is dangerous.");
        }
    }

    public override string ToString()
    {
        var description = base.ToString();
        description += $"IsHazardous: {IsHazardous}\n";
        return description;
    }
    
}