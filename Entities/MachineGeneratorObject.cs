namespace MachinePark.Entities
{
    public class MachineGeneratorObject
    {
        
            public MachineGeneratorObject(string serialNumber, string machineType)
            {
                SerialNumber = serialNumber;
                MachineType = machineType;
            }
            public string SerialNumber { get; set; }
            public string? MachineType { get; set; }
        
    }
}
