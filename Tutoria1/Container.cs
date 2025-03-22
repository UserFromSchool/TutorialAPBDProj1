namespace Tutoria1;

using System;

public abstract class Container
{
    private static int _lastUsedNumber = 0;
    
    public double CargoMass { get; protected set; }  // in kilograms
    public double Height { get; init; }  // in centimeters
    public double TareWeight { get; init; }  // in kilograms
    public double Depth { get; init; }  // in centimeters
    protected double MaxPayload { get; init; }  // in kilograms
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
    
    public string GetSerialNumber()
    {
        return $"KON-{GetContainerType()}-{Id}";
    }
    
    protected abstract string GetContainerType();
    
    public virtual void EmptyCargo()
    {
        CargoMass = 0;
    }
    
    public virtual void LoadCargo(double mass)
    {
        double newCargoMass = CargoMass + mass;
        if (newCargoMass > MaxPayload)
        {
            throw new OverfillException($"Cannot load {mass} kg. Maximum payload is {MaxPayload} kg.");
        }
        CargoMass = newCargoMass;
    }

    public override string ToString()
    {
        var description = "";
        description += $"Container: {GetSerialNumber()}\n";
        description += $"CargoMass: {CargoMass}\n";
        description += $"Height: {Height}\n";
        description += $"TareWeight: {TareWeight}\n";
        description += $"Depth: {Depth}\n";
        description += $"MaxPayload: {MaxPayload}\n";
        return description;
    }

    public override bool Equals(object obj)
    {
        if (obj is Container container)
        {
            return container.GetSerialNumber() == GetSerialNumber();
        }

        return false;
    }

    public override int GetHashCode()
    {
        return GetSerialNumber().GetHashCode();
    }
}


public class OverfillException : Exception
{
    public OverfillException(string message) : base(message) {}
}