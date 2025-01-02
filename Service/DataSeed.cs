using Bogus;
using Bogus.DataSets;
using MachinePark.Entities;
using MachinePark.Service;
using System;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using static MachinePark.Service.DataSeed;
using Machine = MachinePark.Entities.Machine;


namespace MachinePark.Service
{
    public class DataSeed
    {
        private Random rng;
        private static List<string> _companyList;
        private static List<string> _serialNumberList;
        private static List<LeaseHolder> _leaseHolders;
        private static List<MachineType> _machineTypes;

        public DataSeed() 
        {
        rng = new Random();

        }
        public  List<Machine> SeedData(int numberOfMachines)
        {


            _machineTypes = InitializeMockMachineTypes() ?? throw new ArgumentNullException(_machineTypes.ToString());
                _companyList = GenerateMockCompanies(numberOfMachines - (rng.Next(numberOfMachines / 2)));
            _leaseHolders = GenerateMockRenters(rng.Next(_companyList.Count, numberOfMachines));
            _serialNumberList = GenerateSerialNumbers(numberOfMachines, rng);

            List<Machine> machineList = GenerateMachines(numberOfMachines);

            SetLeaseStatus(machineList, rng);
            
            return machineList;
        }

        private void SetLeaseStatus(List<Machine> machineList, Random rng)
        {
            DateTime start = new DateTime(2024, 5, 1);
            DateTime end = DateTime.Now;
            int range = (end - start).Days;
            int maxLeaseDuration = 320;
            int minLeaseDuration = 80;
            foreach (var machine in machineList)
            {
                if (rng.Next(0, 6) < 3)
                {
                    machine.OnLease = true;
                    machine.LeaseStart = start.AddDays(rng.Next(range));
                    machine.LeaseDuration = TimeSpan.FromDays(rng.Next(minLeaseDuration, maxLeaseDuration));
                    machine.LeaseHolder = _leaseHolders[rng.Next(0, (_leaseHolders.Count-1))];
                }
            }
            return;
        }

        private List<Machine> GenerateMachines(int numberOfMachines)
        {
            int machineId = 0;
            int parkingSpot = numberOfMachines;
            var faker = new Faker<Machine>("sv").Rules((f, m) =>
            {
                m.SerialNumber = _serialNumberList[machineId];
                m.MachineType = _machineTypes[f.Random.Int(0, (_machineTypes.Count - 1))];
                m.Id = machineId++;
                m.ParkingSpot = parkingSpot--;

            });
            return faker.Generate(numberOfMachines);
        }

        private List<string> GenerateMockCompanies(int numberOfCompanies)
        {
           
           
            var faker = new Faker<Company>("sv").Rules((f, s) => 
            {
                s.Name = f.Company.CompanyName();
            });
            List<Company> companyList = faker.Generate(numberOfCompanies);
            return companyList.Select(c =>c.Name).ToList();   
        }
        private class Company
        {
            //Temp class to make faker work
            public string Name;
        }

       

        private List<LeaseHolder> GenerateMockRenters(int numberOfRenters)
        {
            int renterId = 0;
            var faker = new Faker<LeaseHolder>("sv").Rules((f, l) => {
                l.Name = f.Person.FullName;
                l.Company = _companyList[f.Random.Int(0, (_companyList.Count - 1))];
                l.Id = renterId++;
            });

            return faker.Generate(numberOfRenters);
        }

        private List<string> GenerateSerialNumbers(int numberOfMachines, Random rng)
        {
            List<string> serialnumberList = new List<string>();
            for (int i = 0; i < numberOfMachines; i++)
            {

            string serialNumber = "";
            for (int j = 0; j < 3; j++)
            {
                int randValue = rng.Next(0, 26);
                char letter = Convert.ToChar(randValue + 65);

                serialNumber += letter;
            }
            for (int k = 0; k < 3; k++)
            {
                int randValue = rng.Next(0, 9);
                serialNumber += randValue;
            } 
            serialnumberList.Add(serialNumber);
        }
            return serialnumberList;
        }



        private static List<MachineType> InitializeMockMachineTypes() => [
            new MachineType{MachineTypeId = 0, MachineTypeName = "Sawmill"},
            new MachineType{MachineTypeId = 1, MachineTypeName = "Grinder"},
            new MachineType{MachineTypeId = 2, MachineTypeName = "Generator"},
            new MachineType{MachineTypeId = 3, MachineTypeName = "Centrifuge"},
            new MachineType{MachineTypeId = 4, MachineTypeName = "Tractor"},
            new MachineType{MachineTypeId = 5, MachineTypeName = "Overhead crane"},
            new MachineType{MachineTypeId = 6, MachineTypeName = "Shredder"},
            new MachineType{MachineTypeId = 7, MachineTypeName = "Band saw"}
            ];
        
        
    }
    
}
