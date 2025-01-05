using MachinePark.Service;
using Microsoft.AspNetCore.Components;

namespace MachinePark.Components.Widgets
{
    public partial class MachineCounter
    {
        [Parameter]
        public int NumberOfMachines { get; set; }

        [Inject]
        MachineService MachineService { get; set; }
        protected override void OnInitialized()
        {
            NumberOfMachines = MachineService.CountMachines();
        }
    }
}
