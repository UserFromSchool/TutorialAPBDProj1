namespace Tutoria1;

using System;

public abstract class Container
{
    private static int _lastUsedNumber = 0;
    
    public double CargoMass { get; protected set; } = 0;  // in kilograms
    public double Height { get; init; }  // in centimeters
    public double TareWeight { get; init; }  // in kilograms
    public double Depth { get; init; }  // in centimeters
    public double MaxPayload { get; init; }  // in kilograms
    public readonly double Id;

    public Container(double height, double tareWeight, double depth, double maxPayload)
    {
        CargoMass = 0;
        Height = height;
        TareWeight = tareWeight;
        Depth = depth;
        MaxPayload = maxPayload;
        Id = _lastUsedNumber++;
    }

    // Generate unique serial number
    public string GetSerialNumber()
    {
        return $"KON-{GetContainerType()}-{Id}";
    }

    // Get the type of the container
    protected abstract string GetContainerType();

    // Method to empty the container
    public virtual void EmptyCargo()
    {
        CargoMass = 0;
    }

    // Method to load cargo
    public virtual void LoadCargo(double mass)
    {
        CargoMass += mass;
        
        if (CargoMass > MaxPayload)
        {
            throw new OverfillException($"Cannot load {mass} kg. Maximum payload is {MaxPayload} kg.");
        }
    }
}

// Custom exception for overfill
public class OverfillException : Exception
{
    public OverfillException(string message) : base(message)
    {
    }
}