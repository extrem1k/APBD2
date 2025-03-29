namespace APBDcw2;

public class LiquidContainer : Container, IHazardNotifier
{
    private static int _serialCounter = 1;
    private bool _isHazardous{ get;  set; }
    private static TypeContainer _type = TypeContainer.Liquid;

    public LiquidContainer(
        
        double height, 
        double weight, 
        double depth, 
        double maxLoadCapacity, 
        bool isHazardous) 
        : base( height, weight, depth, maxLoadCapacity,_type,_serialCounter)
    {
       _isHazardous = isHazardous;
        _serialCounter++;
    }

    public override void Load(string cargo, double mass)
    {
        
        double maxLoad = _isHazardous ? MaxLoadCapacity * 0.5 : MaxLoadCapacity*0.9;

        if (LoadMass + mass > maxLoad)
        {
            
          NotifyHazardousIncident( "Attempted to overfill hazardous liquid container");
          throw new OverFillException($"Cannot load {mass} kg. Exceeds safe capacity of {maxLoad} kg.");
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
        return base.ToString() +$"Niebiezpieczny: {_isHazardous}";
    }
}