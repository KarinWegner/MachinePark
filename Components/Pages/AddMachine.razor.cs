using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MachinePark.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace MachinePark.Components.Pages
{
    public partial class AddMachine
    {

        public static List<string> MachineTypeList;
        private EditContext? editContext;
        private MachineModel Machine = new MachineModel();
        private string FormId = "formId";
        protected override void OnInitialized()
        {
            MachineService.OnChange += StateHasChanged;
            Machine = new MachineModel();
            MachineTypeList = MachineService.GetMachineTypeNames();
            editContext = new(Machine);
        }
        private void addMachine()
        {
            
            MachineService.AddMachine(Machine.SerialNumber, Machine.MachineType);
        }
        public void Dispose()
        {
            MachineService.OnChange -= StateHasChanged;
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
                    if (!MachineTypeList.Any(m => m == MachineType) )
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
