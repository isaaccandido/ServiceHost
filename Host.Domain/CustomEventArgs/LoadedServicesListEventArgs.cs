using Host.Domain.Interfaces;

namespace Host.Domain.CustomEventArgs
{
    public class LoadedServicesListEventArgs : EventArgs
    {
        public List<IService> Services { get; set; }

        public LoadedServicesListEventArgs(IEnumerable<IService> services)
        {
            this.Services = services.ToList();
        }
    }
}
