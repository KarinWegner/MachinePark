using MachinePark.Entities;
using Microsoft.AspNetCore.Components;
using System.Reflection.PortableExecutable;
using Machine = MachinePark.Entities.Machine;

namespace MachinePark.Service
{
    public class MachineService
    {
        
        MachineStorageService MachineStorageService { get; set; }
        int startingId;

        public MachineService(MachineStorageService machineStorageService) 
        {
            
            startingId = MachineList.Count;
        }

        
        private int nextId;
        public int GetNextId()
        {
            if (nextId==0)
            {
                nextId = startingId;
                if (nextId==0)
                {
                    throw new ArgumentException("Id not set properly");
                }
                return nextId++;
            }
            else
            {
                return nextId++;
            }
        }
        private List<Machine> MachineList => MachineStorageService.Machines;
       
        public async Task AddMachine(string serialNumber, string machineType)
        {
           
            var newMachinesType = MachineStorageService.MachineTypes.FirstOrDefault(t=>t.MachineTypeName==machineType);
            if (newMachinesType == null)
            {
                throw new ArgumentNullException("Submitted machinetype does not exist in list.");
            }
            var newMachine = new Machine{ 
            SerialNumber = serialNumber,
            MachineType = newMachinesType,
            ParkingSpot = 5,
            Id= GetNextId()};
            MachineList.Add(newMachine);
        }
        public async Task<Machine> EditMachine(int id, string serialNumber, string machineType)
        {
            Machine editedMachine = new Machine();

            return editedMachine;
        }
            public void DeleteMachine(int id)
        {
            Machine machineToDelete = MachineList.FirstOrDefault(x => x.Id == id);
            if (machineToDelete == null)
            {
                throw new Exception("Machine not found");
            }

            MachineList.Remove(machineToDelete);
            NotifyStateChanged();

        }
        public async Task<bool> MachineExists(int id)
        {
            return MachineList.Any(m => m.Id == id);
        }
        public async Task<IEnumerable<Machine>> GetMachines()
        {
            IEnumerable<Machine> machineList = MachineList.AsEnumerable();
            return machineList.ToList();
        }

        public IEnumerable<string> GetMachineTypeNames()
        {
            return MachineStorageService.MachineTypes.Select(t=>t.MachineTypeName).ToList();
            
        }

        public event Action? OnChange;
        private void NotifyStateChanged()
            {
                OnChange?.Invoke();
            }

        internal int CountMachines()
        {
            return MachineList.Count();
        }

        internal int CountActiveMachines()
        {
            return MachineList.Where(m =>m.IsRunning == true).Count();
        }

        internal int CountAvailableMachines()
        {
            return MachineList.Where(m => m.OnLease == true).Count();
        }
    }

}
