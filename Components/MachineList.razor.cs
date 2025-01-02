using MachinePark.Entities;
using MachinePark.Service;

namespace MachinePark.Components
{
    public partial class MachineList
    {
        public List<Machine> machineList { get; set; } = default!;

        protected override void OnInitialized()
        {
            Task.Delay(2000);
            machineList = MachineStorageService.Machines;
        }
    }
}
