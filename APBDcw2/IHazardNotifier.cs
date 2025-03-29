namespace APBDcw2;

public interface IHazardNotifier
{
    void NotifyHazardousIncident(string containerId, string details)
    {
        Console.WriteLine($"Notifying hazardous incident: {details}"+ $" in container: {containerId}");
    }
}