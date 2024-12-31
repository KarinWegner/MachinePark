using System.Security.Cryptography;
using MachinePark.Entities;
using MachinePark.Service;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MachinePark.Service
{
    public class MachineStorageService
    {
        public static List<Machine> MachineGarage;
        public int NextId;
        private DataSeed dataSeed;

        public MachineStorageService() 
        {
            if (MachineGarage == null) 
            MachineGarage = new List<Machine>();
            dataSeed = new DataSeed();
            SeedMachines(10);
        }
        public event Action? OnChange;
        private int GetParkingSpot()
        {
            return MachineGarage.Count()+1;
        }
        public IEnumerable<Machine> GetMachineList()
        {
            return MachineGarage;
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
        public void DeleteMachine(int id)
        {
            Machine machineToDelete = MachineGarage.FirstOrDefault(x => x.Id == id);
            if (machineToDelete == null)
            {
                throw new Exception("Machine not found");
            }
            
            MachineGarage.Remove(machineToDelete);
            NotifyStateChanged();

        }

        public void EditMachine(int id, string serialNumber, string machineType)
        {
            Machine machineToEdit = MachineGarage.FirstOrDefault(x => x.Id == id);
            if (machineToEdit == null)
            {
                throw new Exception("Machine not found");
            }
            machineToEdit.SerialNumber = serialNumber;
            machineToEdit.MachineType = machineType;


        }
        public bool MachineExists(int id)
        {
           return MachineGarage.Any(m => m.Id == id);
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
     


       

    }
}
