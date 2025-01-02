using System.Security.Cryptography;
using MachinePark.Entities;
using MachinePark.Service;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MachinePark.Service
{
    public class MachineStorageService
    {
        public List<Machine> _machineGarage;
        public int NextId;
        private DataSeed dataSeed;
        public static List<Machine> Machines;
        public static List<MachineType > MachineTypes;

        public MachineStorageService() 
        {            
            dataSeed = new DataSeed();
            Machines = dataSeed.SeedData(30);
            MachineTypes = dataSeed.GetMachineTypes();
            NextId = Machines.Count;
            
        }
        
        public event Action? OnChange;


       

        public void DeleteMachine(int id)
        {
            Machine machineToDelete = Machines.FirstOrDefault(x => x.Id == id);
            if (machineToDelete == null)
            {
                throw new Exception("Machine not found");
            }
            
            Machines.Remove(machineToDelete);
            NotifyStateChanged();

        }
        public void AddMachine(string serialNumber, MachineType machineType)
        {
            Machine newMachine = new Machine(){
                SerialNumber = serialNumber,
                MachineType = machineType,
                Id = NextId++
            };
            Machines.Add(newMachine);
        }
        public void EditMachine(int id, string serialNumber, string machineType)
        {
            

        }
        public bool MachineExists(int id)
        {
           return Machines.Any(m => m.Id == id);
        }
        //public async Task SeedMachines(int numberOfMachines)
        //{
        //   Machines= await dataSeed.SeedData(numberOfMachines);
            
        //}
        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }

        internal List<MachineType> GetMachineTypes()
        {
            return MachineTypes;
        }
    }
}
