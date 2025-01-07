using System.ComponentModel.DataAnnotations;

namespace MachinePark.Request
{
    public class RequestParams
    {
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;
        [Range(1, 20)]
        public int PageSize { get; set; } = 10;
    }
}
