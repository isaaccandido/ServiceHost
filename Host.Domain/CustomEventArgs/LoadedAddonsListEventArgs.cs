using Host.Domain.Interfaces;

namespace Host.Domain.CustomEventArgs
{
    public class LoadedAddonsListEventArgs : EventArgs
    {
        public List<IAddon> Services { get; set; }

        public LoadedAddonsListEventArgs(IEnumerable<IAddon> services)
        {
            this.Services = services.ToList();
        }
    }
}
