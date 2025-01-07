using System.Linq.Expressions;
using System.Security.Cryptography;
using MachinePark.Entities;
using MachinePark.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Query;

namespace MachinePark.Service
{
    public class MachineStorageService : IAsyncQueryProvider
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

        public async Task AddMachine(Machine newMachine)
        {
            if (newMachine == null) throw new ArgumentNullException("No machine was submitted");

            Machines.Add(newMachine);
           // NotifyStateChanged();
        }
        public void DeleteMachine(int machineId)
        {
            Machine machineToDelete = Machines.FirstOrDefault(x => x.Id == machineId);
            if (machineToDelete == null) throw new ArgumentNullException("Machine does not exist in database");

            Machines.Remove(machineToDelete);
        }
        public static List<Machine> GetGeneratedMachines()
        {
            return Machines;
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
        public IQueryable<Machine> GetMachineQuery(List<Machine> machineList)
        {
            return machineList.AsQueryable();
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            throw new NotImplementedException();
        }

        public object? Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}
