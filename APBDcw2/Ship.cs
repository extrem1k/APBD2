namespace APBDcw2;

using System;
using System.Collections.Generic;
using System.Linq;

public class Ship
{
    // Ship properties
    public string Name { get;  set; }
    public double Speed { get; set; }
    public int MaxContainerNum { get;  set; }
    public double MaxWeight { get; set; }

    // Lists to manage containers
    public List<Container> _containers= new List<Container>();

    
   

    
    public void LoadContainer(Container container)
    {
        
        if (_containers.Count >= MaxContainerNum)
        {
            throw new InvalidOperationException("too much containers");
        }

        
        if (_containers.Sum(c => c.Weight+c.LoadMass) + container.Weight +container.LoadMass    >     MaxWeight*1000)
        {
            throw new InvalidOperationException("too much weight");
        }

        _containers.Add(container);
    }

    public void UnloadContainer(Container container)
    {
        if (!_containers.Contains(container))
        {
            throw new ArgumentException("Container is not on this ship.");
        }

        _containers.Remove(container);
    }

    

    // Method to move a container between ships
    public void MoveContainerToAnotherShip(Ship destinationShip, Container container)
    {
        
        // Load to destination ship
        destinationShip.LoadContainer(container);
        // Unload from current ship
        UnloadContainer(container);

        
    }

    // Method to get information about the ship and its containers
    public void DisplayShipInfo()
    {
        Console.WriteLine($"Ship Name: {Name}");
        Console.WriteLine($"Speed: {Speed} km/h");
        Console.WriteLine($"Max Containers: {MaxContainerNum}");
        Console.WriteLine($"Max Weight: {MaxWeight} tons");
        Console.WriteLine($"Current Containers: {_containers.Count}");
        Console.WriteLine($"Current  Weight: {_containers.Sum(c => c.Weight)} tons");

        Console.WriteLine("\nContainer Details:");
        for (int i = 0; i < _containers.Count; i++)
        {
            Container container = _containers[i];
            Console.WriteLine($"Container {i + 1}:");
            Console.WriteLine($"  Serial Number: {container.SerialNumber}");
            Console.WriteLine($"  Current Load: {container.LoadMass} kg");
            Console.WriteLine($"  Max Load Capacity: {container.MaxLoadCapacity} kg");
            Console.WriteLine("/////////");
        }
    }
    
}