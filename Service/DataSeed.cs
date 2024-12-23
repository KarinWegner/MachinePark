using MachinePark.Entities;
using MachinePark.Service;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using static MachinePark.Service.DataSeed;
using Machine = MachinePark.Entities.Machine;


namespace MachinePark.Service
{
    public class DataSeed
    {
        private Random rng; 
        public DataSeed() 
        {
        rng = new Random();
        }
        public async Task<IEnumerable<MachineGeneratorObject>> GenerateMachines(int numberOfMachines)
        {
            var machineBuilderList = new List<MachineGeneratorObject>();

            string[] machineTypes = new string[] { "Tractor", "Industrial Saw", "SawMill", "Belt Sander" };

            for (int i = 0; i < numberOfMachines; i++)
            {
                string serialNumber = "";
                string machineType = machineTypes[rng.Next(machineTypes.Length - 1)];
                for (int j = 0; j < 3; j++)
                {
                    int randValue = rng.Next(0,26);
                    char letter = Convert.ToChar(randValue+65);

                    serialNumber += letter;
                }
                for (int k = 0; k < 3; k++)
                {
                    int randValue = rng.Next(0,9);
                    serialNumber += randValue;
                }
                machineBuilderList.Add(new MachineGeneratorObject(serialNumber, machineType));
            }

            return machineBuilderList;

        }
        
    }
    
}
