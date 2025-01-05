using MachinePark.Service;
using Microsoft.AspNetCore.Components;

namespace MachinePark.Components.Widgets
{
    public partial class AvailableMachineCounter
    {
        [Parameter]
        public int AvailableMachines { get; set; }

        [Inject]
        MachineService MachineService { get; set; }
        protected override void OnInitialized()
        {
            AvailableMachines = MachineService.CountAvailableMachines();
        }
    }
}
