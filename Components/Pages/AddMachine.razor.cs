using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MachinePark.Entities;

namespace MachinePark.Components.Pages
{
    public partial class AddMachine
    {

        public static List<MachineType> MachineTypes;
        protected override void OnInitialized()
        {
            MachineStorageService.OnChange += StateHasChanged;
            Machine = new MachineModel();
            MachineTypes = MachineStorageService.GetMachineTypes();
        }
        private void addMachine()
        {
            MachineType machineType = MachineTypes.FirstOrDefault(m => m.MachineTypeName ==Machine.MachineType);
            MachineStorageService.AddMachine(Machine.SerialNumber, machineType);
        }
        public void Dispose()
        {
            MachineStorageService.OnChange -= StateHasChanged;
        }

        public class MachineModel : IValidatableObject
        {

            [Required(ErrorMessage = "A Serial number is required")]
            [Length(4, 16, ErrorMessage = "Serial number has to be between 4 and 16 characters")]
            public string? SerialNumber { get; set; }
            [DisplayName("Machine Type")]
            [Required(ErrorMessage ="Please select a machine type")]

            [Length(4, 16, ErrorMessage = "macihne type has to be between 1 and 16 characters")]
            public string? MachineType { get; set; }


            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                if (string.IsNullOrWhiteSpace(SerialNumber))
                {
                    yield return new ValidationResult(
                        "Your machine needs a Serial number.",
                        new[] { nameof(SerialNumber) });

                    if (SerialNumber.Count() > 3 || SerialNumber.Count() < 16)
                    {
                        yield return new ValidationResult(
                        "Serial number has to be between 1 and 16 characters.",
                        new[] { nameof(SerialNumber) });
                    }
                }
                if (MachineType != null)
                {
                    if (MachineType.Count() > 16 || MachineType.Count() < 1)
                    {
                        yield return new ValidationResult(
                            "macihne type has to be between 1 and 16 characters",
                            new[] { nameof(MachineType) });
                    }
                    if (!MachineTypes.Any(m => m.MachineTypeName == MachineType) )
                    {
                        yield return new ValidationResult(
                            "Machine does not belong to allowed machinetype group",
                            new[] { nameof(MachineType) });
                    }
                }
            }
        }
    }
}
