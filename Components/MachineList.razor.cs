using MachinePark.Entities;
using MachinePark.Service;
using MachinePark.Request;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace MachinePark.Components
{
    public partial class MachineList
    {
        public List<Machine> machineList { get; set; } = [];

        private float itemHeight = 50;
        [Parameter]
        public int CurrentPage { get; set; } = 1;
        public int TotalPages {  get; set; } 
        [Parameter]
        public int PageSize { get; set; } 
       
        protected override async Task OnInitializedAsync()
        {
            Task.Delay(2000);
            PageSize = MachineService.RequestParams.PageSize;
            CurrentPage=MachineService.RequestParams.PageNumber;
            machineList = await MachineService.GetPagedMachineList(PageSize, CurrentPage);

        }
        //public async Task<List<Machine>>
        //    LoadMachines(int pageSize, int pageNumber)
        //{
        //    int start = PageSize * CurrentPage;
        //    return await MachineService.GetPagedMachineList(pageSize, pageNumber);
           
        //}
    }
}
