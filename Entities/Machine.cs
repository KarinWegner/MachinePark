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
        [Required(ErrorMessage="A Machine Type needs to be selected")]
        [DisplayName("Machine Type")]
            public MachineType MachineType { get; set; }

        [DisplayName("Is Active")]
            public bool IsRunning { get; set; } = false;
        [DisplayName("Parking spot")]
            public int ParkingSpot { get; set; }

        [DisplayName("On Lease")]
        public bool OnLease { get; set; } = false;

        [DisplayName("Lease Holder")]
        public LeaseHolder? LeaseHolder { get; set; } = default!;

        [DisplayName("Lease start")]
        [DataType(DataType.DateTime, ErrorMessage ="Lease Start is not in DateTime format")]
        public DateTime? LeaseStart { get; set; } = default!;

        [DisplayFormat(DataFormatString = @"{0:%d} days {0:%h} hours {0:%m} minutes")]
        [DisplayName("Lease duration")]
        public TimeSpan? LeaseDuration { get; set; } = default!;

        [DisplayName("Lease end")]
        public DateTime? LeaseEnd { get { return LeaseStart + LeaseDuration; } }

        [DisplayName("Time left")]
        [DisplayFormat(DataFormatString = @"{0:%d} days {0:%h} hours {0:%m} minutes")]
        public TimeSpan? LeaseTimeLeft { get { return LeaseEnd - DateTime.UtcNow; } }
        
    }
}
