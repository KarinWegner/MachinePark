using MachinePark.Entities;
using MachinePark.Service;


namespace MachinePark.Service
{
    public class PageManager
    {
        public PageManager() 
        {
        }
        public int SelectedMachineId { get; set; }

        public void SetSelected(int id)
        {
          
                SelectedMachineId = id;
                NotifyStateChanged();
            
        }

        public event Action? OnChange;
        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
