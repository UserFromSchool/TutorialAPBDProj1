namespace Tutoria1;

public class RefrigeratedContainer : Container
{
    
    public string Product { get; protected set; }

    public RefrigeratedContainer(double height, double tareWeight, double depth, double maxPayload, string product) :
        base(height, tareWeight, depth, maxPayload)
    {
        Product = product;
    }

    protected override string GetContainerType()
    {
        return "C";
    }
    
    public void LoadCargo(double mass, string product = "")
    {
        if (product != Product)
        {
            throw new Exception("Can't load different product onto the Refrigerated Container!");
        }
        
        CargoMass += mass;
        
        if (CargoMass > MaxPayload)
        {
            throw new OverfillException($"Cannot load {mass} kg. Maximum payload is {MaxPayload} kg.");
        }
    }
    
}