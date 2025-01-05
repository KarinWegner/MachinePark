using MachinePark.Components.Widgets;

namespace MachinePark.Components.Pages
{
    public partial class Home
    {
        public List<Type> Widgets { get; set; } = new List<Type> { typeof(MachineCounter), typeof(AvailableMachineCounter), typeof(RunningMachineCounter) };
    }
}
