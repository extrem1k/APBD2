namespace APBDcw2;

// Gas Container (G)
public class GasContainer : Container, IHazardNotifier
{

    private double _pressure{get;set;}
    private static int _serialCounter = 1;
    private static TypeContainer _type = TypeContainer.Gas;
    public GasContainer(
        double pressure,
        double height,
        double weight,
        double depth,
        double maxLoadCapacity
    )
        : base( height, weight, depth, maxLoadCapacity,_type,_serialCounter)
    {
        _pressure = pressure;
        _serialCounter++; 
    }


    public override void Unload()
    {
       
        LoadMass *= 0.05;
    }

    public override void Load(string cargo, double mass)
    {
        if (LoadMass + mass > MaxLoadCapacity)
        {
            NotifyHazardousIncident($"too much pressure");
        }
        _cargo.Add(cargo);
        LoadMass += mass;
    }
    public void NotifyHazardousIncident( string details)
    {
        Console.WriteLine($"Notifying hazardous incident: {details}"+ $" in container: {SerialNumber}");
    }

    public override string ToString()
    {
       return base.ToString()+ $"pressure: { _pressure} ";
    }
    
}
