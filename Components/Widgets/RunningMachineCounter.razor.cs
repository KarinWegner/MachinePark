using MachinePark.Service;
using Microsoft.AspNetCore.Components;

namespace MachinePark.Components.Widgets
{
    public partial class RunningMachineCounter
    {
        [Parameter]
        public int RunningMachineCount { get; set; }
        [Inject]
        MachineService MachineService { get; set; }
        protected override void OnInitialized()
        {
            RunningMachineCount = MachineService.CountActiveMachines();
        }
    }
}
