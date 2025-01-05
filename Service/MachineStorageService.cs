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
        
        //public event Action? OnChange;

        public void AddMachine(Machine newMachine)
        {
            if (newMachine == null) throw new ArgumentNullException("No machine was submitted");

            Machines.Add(newMachine);
          //  NotifyStateChanged();
        }


       

       
       
       
       
        //public async Task SeedMachines(int numberOfMachines)
        //{
        //   Machines= await dataSeed.SeedData(numberOfMachines);
            
        //}
        //private void NotifyStateChanged()
        //{
        //    OnChange?.Invoke();
        //}

        internal List<MachineType> GetMachineTypes()
        {
            return MachineTypes;
        }
    }
}
