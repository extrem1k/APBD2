


using APBDcw2;

public class Program
{
    private List<Ship> ships = new List<Ship>();
    private List<Container> containers = new List<Container>();
    private List<Container> _freeContainers = new List<Container>();

    static void Main(string[] args)
    {
        var app = new Program();
        app.Run();
    }

    private void DisplayMainMenu()
    {
        Console.Clear();
        Console.WriteLine("===== SYSTEM ZARZĄDZANIA KONTENEROWCAMI =====");
        Console.WriteLine("Możliwe akcje:");
        Console.WriteLine("1. Dodaj kontenerowiec");
        Console.WriteLine("2. Dodaj kontener");
        Console.WriteLine("3. Usuń kontener");
        Console.WriteLine("4. Załaduj kontener na statek");
        Console.WriteLine("5. Wyświetl listę kontenerowców");
        Console.WriteLine("6. Wyświetl listę kontenerów");
        Console.WriteLine("7. Zarządzaj ładunkiem kontenera");
        Console.WriteLine("8. Usuń kontener ze statku");
        Console.WriteLine("0. Wyjście");
        Console.Write("Wybierz opcję: ");
    }
    
    
    
    
    
    
    private void AddShip()
    {
        Console.Clear();
        try 
        {
            Console.Write("Podaj nazwę statku: ");
            string name = Console.ReadLine();

            Console.Write("Podaj maksymalną prędkość (węzły): ");
            double speed = double.Parse(Console.ReadLine());

            Console.Write("Podaj maksymalną liczbę kontenerów: ");
            int maxContainers = int.Parse(Console.ReadLine());

            Console.Write("Podaj maksymalną wagę ładunku (tony): ");
            double maxWeight = double.Parse(Console.ReadLine());

            if(ships.Any(c => c.Name == name))
                throw new Exception($"Nazwa statku: {name} istnieje");
            var Ship = new Ship {
                Name = name,
                Speed = speed,
                MaxContainerNum = maxContainers,
                MaxWeight = maxWeight
            };

           ships.Add(Ship);
            Console.WriteLine($"Dodano kontenerowiec: {name}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
        Console.ReadKey();
    }
    
    private void AddContainer()
    {
        Console.Clear();
        try 
        {
            
            Console.Write("Podaj maksymalną pojemność (kg): ");
            double maxCapacity = double.Parse(Console.ReadLine());
            
            Console.Write("Podaj wagę własną kontenera (kg): ");
            double weight = double.Parse(Console.ReadLine());

            Console.Write("Podaj wysokosc własną kontenera : ");
            double height = double.Parse(Console.ReadLine());

            Console.Write("Podaj glebokosc własną kontenera : ");
            double depth = double.Parse(Console.ReadLine());
            
            
            Console.WriteLine("Wybierz typ kontenera:");
            Console.WriteLine("1. Chłodniczy");
            Console.WriteLine("2. Na płyny");
            Console.WriteLine("3. Na gaz");
            Console.Write("Wybór: ");
            
            int typeChoice = int.Parse(Console.ReadLine());
            Container container = null;
            switch (typeChoice)
            {
                case 1 : 
                    Console.Write("Jaki produkt:  ");
                    string produkt= Console.ReadLine();
                    container= new RefrigeratedContainer(height, weight, depth, maxCapacity,produkt );
                    break;
                case 2:  Console.Write("Czy kontener jest niebezpieczny?  ");
                    string taknie = Console.ReadLine();
                    bool czyniebezpieczny = taknie.ToLower().Equals("tak")?true:false;
                    container = new LiquidContainer(height, weight, depth, maxCapacity, czyniebezpieczny);
                    break;
                case 3:    Console.Write("Podaj cisnienie kontenera : ");
                    double pressure = double.Parse(Console.ReadLine());
                    container = new GasContainer(pressure, height, weight, depth, maxCapacity);
                        break;
                default: throw new ArgumentException("Nieprawidłowy typ kontenera");
            };


            containers.Add(container);
            _freeContainers.Add(container);
            
            Console.WriteLine($"Dodano kontener: {container.SerialNumber}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
        Console.ReadKey();
    }
    
    private void RemoveContainer()
    {
        DisplayContainers();
        
        if (containers.Count == 0)
        {
            Console.WriteLine("Brak kontenerów do usunięcia.");
            Console.ReadKey();
            return;
        }

        Console.Write("Podaj numer seryjny kontenera do usunięcia: ");
        string serialNumber = Console.ReadLine();

        var container = containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container != null)
        {
            containers.Remove(container);
            _freeContainers.Remove(container);
            Console.WriteLine($"Usunięto kontener: {serialNumber}");
        }
        else
        {
            Console.WriteLine("Nie znaleziono kontenera o podanym numerze.");
        }
        Console.ReadKey();
    }
    
    private void LoadContainerToShip()
    {
        if (ships.Count == 0 || _freeContainers.Count == 0)
        {
            Console.WriteLine("Brak statków lub wolnych kontenerów.");
            Console.ReadKey();
            return;
        }

        DisplayShips();
        Console.Write("Wybierz statek (nazwa): ");
        string vesselName = Console.ReadLine();

        var ship = ships.FirstOrDefault(v => v.Name == vesselName);
        if (ship == null)
        {
            Console.WriteLine("Nie znaleziono statku.");
            Console.ReadKey();
            return;
        }

        DisplayFreeContainers();
        Console.Write("Wybierz kontener (numer seryjny): ");
        string containerSerial = Console.ReadLine();

        var container = containers.FirstOrDefault(c => c.SerialNumber == containerSerial);
        if (container == null)
        {
            Console.WriteLine("Nie znaleziono kontenera.");
            Console.ReadKey();
            return;
        }
      
        try 
        {
           
            ship.LoadContainer(container);
            _freeContainers.Remove(container);
            Console.WriteLine($"Załadowano kontener {containerSerial} na statek {vesselName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd ładowania: {ex.Message}");
        }
        Console.ReadKey();
    }
    
    private void ManageContainerCargo()
    {
        DisplayContainers();
        
        if (containers.Count == 0)
        {
            Console.WriteLine("Brak kontenerów.");
            Console.ReadKey();
            return;
        }

        Console.Write("Podaj numer seryjny kontenera: ");
        string serialNumber = Console.ReadLine();

        var container = containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container == null)
        {
            Console.WriteLine("Nie znaleziono kontenera.");
            Console.ReadKey();
            return;
        }

        while (true)
        {
            Console.Clear();
            Console.Write(container);
            Console.WriteLine("\nOperacje na ładunku:");
            Console.WriteLine("1. Dodaj ładunek");
            Console.WriteLine("2.  rozładuj");
            Console.WriteLine("0. Powrót");
            Console.Write("Wybierz opcję: ");

            string choice = Console.ReadLine();
            
            try 
            {
                switch (choice)
                {
                    case "1":
                        AddCargo(container);
                        break;
                    case "2":
                        container.Unload();
                        if(container.TypeContainer==TypeContainer.Gas)
                            Console.WriteLine("Kontener rozładowany do 5%");
                        else
                            Console.WriteLine("Kontener całkowicie rozładowany.");
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
            
            Console.ReadKey();
        }
    }
    private void AddCargo(Container container)
    {
        Console.Write("Podaj nazwę ładunku: ");
        string cargoName = Console.ReadLine();

        Console.Write("Podaj masę ładunku (kg): ");
        double mass = double.Parse(Console.ReadLine());

        container.Load(cargoName, mass);
        Console.WriteLine($"Dodano {mass} kg {cargoName} do kontenera {container.SerialNumber}");
    }

    public void DeleteContainerFromShip()
    {
        if (ships.Count == 0 || containers.Count == 0)
        {
            Console.WriteLine("Brak statków lub kontenerów.");
            Console.ReadKey();
            return;
        }

        DisplayShips();
        Console.Write("Wybierz statek (nazwa): ");
        string vesselName = Console.ReadLine();

        var ship = ships.FirstOrDefault(v => v.Name == vesselName);
        if (ships == null)
        {
            Console.WriteLine("Nie znaleziono statku.");
            Console.ReadKey();
            return;
        }
        foreach (var cont in ship._containers)
        {
            Console.WriteLine($"Nazwa kontenera: {cont.SerialNumber}");
        }
        
        Console.Write("Wybierz kontener (numer seryjny) do usunięcia: ");
        string containerSerial = Console.ReadLine();

        var container = containers.FirstOrDefault(c => c.SerialNumber == containerSerial);
        if (container == null)
        {
            Console.WriteLine("Nie znaleziono kontenera.");
            Console.ReadKey();
            return;
        }
      
        try 
        {
           
            ship.UnloadContainer(container);
            _freeContainers.Add(container);
            Console.WriteLine($" Usunięto kontener {containerSerial} ze statku {vesselName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd ładowania: {ex.Message}");
        }
        Console.ReadKey();
        
        
        
        
    }
    
    
    public void Run()
    {
        while (true)
        {
            DisplayMainMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddShip();
                    break;
                case "2":
                    AddContainer();
                    break;
                case "3":
                    RemoveContainer();
                    break;
                case "4":
                    LoadContainerToShip();
                    break;
                case "5":
                    DisplayShips();
                    break;
                case "6":
                    DisplayContainers();
                    break;
                case "7":
                    ManageContainerCargo();
                    break;
                case "8":
                    DeleteContainerFromShip();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                    break;
            }
        }
    }
    private void DisplayFreeContainers()
    {
        Console.Clear();
        if (containers.Count == 0)
        {
            Console.WriteLine("Brak kontenerów.");
        }
        else
        {
            Console.WriteLine("Lista kontenerów:");
            foreach (var container in _freeContainers)
            {
                Console.WriteLine(container);
            }
            Console.ReadKey(); 
        }
        
    }
    private void DisplayContainers()
    {
        Console.Clear();
        if (containers.Count == 0)
        {
            Console.WriteLine("Brak kontenerów.");
        }
        else
        {
            Console.WriteLine("Lista kontenerów:");
            foreach (var container in containers)
            {
                Console.WriteLine(container);
        }
             Console.ReadKey(); 
        }
        
    }
    
    private void DisplayShips()
    {
        Console.Clear();
        if (ships.Count == 0)
        {
            Console.WriteLine("Brak kontenerowców.");
        }
        else
        {
            Console.WriteLine("Lista kontenerowców:");
            foreach (var ship in ships)
            {
                ship.DisplayShipInfo();
            }
        }
        Console.ReadKey();
    }
    
    
}




