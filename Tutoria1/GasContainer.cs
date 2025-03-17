namespace Tutoria1;

public class GasContainer : Container, IHazardousNotifier
{
    
    public GasContainer(double height, double tareWeight, double depth, double maxPayload) :
        base(height, tareWeight, depth, maxPayload) {}

    public void SendNotificationAboutHazardousOccurance(string message)
    {
        Console.WriteLine($"{GetSerialNumber()} (Hazardous Event Caught!) : {message}");
    }

    protected override string GetContainerType()
    {
        return "G";
    }
    
    public override void EmptyCargo()
    {
        CargoMass = 0.05 * CargoMass;
    }
    
}