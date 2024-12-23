using System.Security.Cryptography;
using MachinePark.Entities;
using MachinePark.Service;

namespace MachinePark.Service
{
    public class MachineStorageService
    {
        public List<Machine> MachineGarage;
        public int NextId;
        private DataSeed dataSeed;

        public MachineStorageService() 
        {
            if (MachineGarage == null) 
            MachineGarage = new List<Machine>();
            dataSeed = new DataSeed();
            SeedMachines(10);
        }
        private int GetParkingSpot()
        {
            return MachineGarage.Count()+1;
        }

        public void AddMachine(string? serialNumber, string? machineType)
        {
            int id = NextId++;
            int parkingSpot = GetParkingSpot();
            Machine newMachine = new Machine
            {
                Id = id,
                SerialNumber = serialNumber,
                MachineType = machineType,
                ParkingSpot = parkingSpot
            };
            MachineGarage.Add(newMachine);

            NotifyStateChanged();
        }
        public async Task SeedMachines(int numberOfMachines)
        {
            IEnumerable<MachineGeneratorObject> seededMachines = await dataSeed.GenerateMachines(numberOfMachines);
            foreach (var machine in seededMachines) 
            {
                AddMachine(machine.SerialNumber, machine.MachineType);
            }
        }
        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
     

        public event Action? OnChange;
       

    }
}
