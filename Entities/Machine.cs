namespace MachinePark.Entities
{
    public class Machine
    {
        
            public int Id { get; set; }
            public string? SerialNumber { get; set; }
            public string? MachineType { get; set; }
            public bool IsRunning { get; set; } = false;
            public int ParkingSpot { get; set; }
        
    }
}
