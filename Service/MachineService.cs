using MachinePark.Entities;
using Microsoft.AspNetCore.Components;
using System.Reflection.PortableExecutable;
using Machine = MachinePark.Entities.Machine;
using Microsoft.EntityFrameworkCore;
using MachinePark.Request;

namespace MachinePark.Service
{
    public class MachineService
    {
        
       
        int startingId;
        MachineStorageService storageService;
        public RequestParams RequestParams;
        public MachineService(MachineStorageService machineStorageService)
        {
            MachineList = MachineStorageService.GetGeneratedMachines();
            int machineCount = MachineList.Count;
            startingId = MachineList.Count();
            RequestParams = new RequestParams();
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
        public List<Machine> MachineList;
        public PagedList<Machine> PagedMachineList;
        public async Task<List<Machine>> GetMachines()
        {
            return MachineList.ToList();
           
        }

        public async Task<List<Machine>> GetPagedMachineList(int pagesize, int start)
        {

            return  MachineList.Skip(start).Take(pagesize).ToList(); 

        }
       
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
            return  MachineList.Any(m => m.Id == id);
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
