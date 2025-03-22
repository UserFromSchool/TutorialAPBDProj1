namespace Tutoria1;

public class RefrigeratedContainer : Container
{

    public readonly string Product;
    public double Temperature { get; protected set; }

    public RefrigeratedContainer(double height, double tareWeight, double depth, double maxPayload,
        string product, double temperature) :
        base(height, tareWeight, depth, maxPayload)
    {
        Product = product;
        Temperature = temperature;
    }

    protected override string GetContainerType()
    {
        return "C";
    }

    public override void LoadCargo(double mass)
    {
        LoadCargo(mass, null);
    }
    
    public void LoadCargo(double mass, string? product)
    {
        if (product != Product)
        {
            if (product == null)
            {
                throw new KeyNotFoundException("Can't load unknown product onto the Refrigerated Container!");
            } 
            throw new KeyNotFoundException("Can't load different product onto the Refrigerated Container!");
        }
        base.LoadCargo(mass);
    }
    
    public override string ToString()
    {
        var description = base.ToString();
        description += $"Product: {Product}\n";
        description += $"Temperature: {Temperature}\n";
        return description;
    }
    
}