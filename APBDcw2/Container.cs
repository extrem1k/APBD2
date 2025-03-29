namespace APBDcw2;

public abstract class Container

{
    public List<string> _cargo { get; private set; } = new List<string>();
    public string SerialNumber { get; private set; }
    public double LoadMass { get; protected set; }
    public double Height { get; private set; }
    public double Weight { get; private set; }
    public double Depth { get; private set; }
    public double MaxLoadCapacity { get; private set; }
    public TypeContainer TypeContainer { get; private set; }

    // Constructor
    protected Container( double height, double weight, double depth, double maxLoadCapacity, TypeContainer typeContainer, int counter)
    {
        
        SerialNumber = GenerateSerialNumber(typeContainer,counter);
        Height = height;
        Weight = weight;
        Depth = depth;
        MaxLoadCapacity = maxLoadCapacity;
        LoadMass = 0;
        TypeContainer = typeContainer;
    }

    private static string GenerateSerialNumber(TypeContainer type,int counter)
    {
        string typePrefix = type switch
        {
            TypeContainer.Liquid => "KON-L",
            TypeContainer.Gas => "KON-G",
            TypeContainer.Refrigerated => "KON-C",
            _ => throw new ArgumentException("Nieprawidłowy typ kontenera")
        };
        return $"{typePrefix}-{counter:D3}";
    }
    public virtual void Load(string cargo, double mass)
    {
        if (LoadMass + mass > MaxLoadCapacity)
        {
            throw new OverFillException($"Cannot load {mass} kg. Exceeds maximum capacity of {MaxLoadCapacity} kg.");
        }
        _cargo.Add(cargo);
        LoadMass += mass;
    }

    
    public virtual void Unload()
    {
        _cargo.Clear();
        LoadMass = 0;
    }


    public override string ToString()
    {
        return
            $"name: {SerialNumber}, mass: {LoadMass}, height: {Height}, weight: {Weight}, depth: {Depth}, max load capacity: {MaxLoadCapacity}, ";
    }

    
}
