using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MachinePark.Entities
{
    public class Machine
    {
        [Required(ErrorMessage = "All machines require an ID")]
            public int Id { get; set; }
        [Required(ErrorMessage ="A Serial number is required")]
        [Length(4,16, ErrorMessage ="Serial number has to be between 4 and 16 characters")]

        [RegularExpression(@"[A-ZÅÄÖ],[0-9]", ErrorMessage = "Serial number can only contain letters and digits")]
        public string? SerialNumber { get; set; }
        [DisplayName("Machine Type")]
            public string? MachineType { get; set; }
        [DisplayName("Is Active")]
            public bool IsRunning { get; set; } = false;
        [DisplayName("Parking spot")]
            public int ParkingSpot { get; set; }
        
    }
}
