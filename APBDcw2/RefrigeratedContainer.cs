namespace APBDcw2;

public class RefrigeratedContainer : Container
{
    private static TypeContainer _type = TypeContainer.Refrigerated;
    private static int _serialCounter = 1;
    private  double _temp = 0;
    public string ProductType { get; private set; }
    

    
    private static readonly Dictionary<string, double> ProductTemperatures = new Dictionary<string, double>
    {
        { "Bananas", 13.3 },
        { "Chocolate", 18.0 },
        { "Fish", 2.0 },
        { "Meat", -15.0 },
        { "Ice cream", -18.0 },
        { "Frozen pizza", -30.0 },
        { "Cheese", 7.2 },
        { "Sausages", 5.0 },
        { "Butter", 20.5 },
        { "Eggs", 19.0 }
    };

    public RefrigeratedContainer(
        double height, 
        double weight, 
        double depth, 
        double maxLoadCapacity,
        string productType) 
        : base( height, weight, depth, maxLoadCapacity, _type,_serialCounter)
    {
        _serialCounter++;
        SetProductType(productType);
    }

    public void SetProductType(string productType)
    {
        if (!ProductTemperatures.ContainsKey(productType))
        {
            _temp = 10;
        }
        else
        

            {
                _temp = ProductTemperatures[productType];
                ProductType = productType;
                _temp = ProductTemperatures[productType];
            }
        }

    public override string ToString()
    {
        return base.ToString()+ $"temperature: {_temp}";
            
            
            ;
    }
}