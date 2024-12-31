using MachinePark.Entities;
using MachinePark.Service;
using Microsoft.AspNetCore.Components;

namespace MachinePark.Components.Pages
{
    public partial class MachineDetails
    {
        [Parameter]
        public int MachineID { get; set; }

        private Machine Machine { get; set; } = new Machine();

        protected override void OnInitialized()
        {
            Machine = MachineStorageService.MachineGarage.Single(m => m.Id == MachineID);
        }
        private void ChangeActiveStatus()
        {
            Machine.IsRunning = !Machine.IsRunning;
        }


    }
}
