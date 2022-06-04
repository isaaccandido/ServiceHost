using Host.Domain.Interfaces;

namespace Host.Domain.CustomEventArgs
{
    public class StoppedServicesListEventArgs : EventArgs
    {
        public List<IService> Services { get; set; }

        public StoppedServicesListEventArgs(IEnumerable<IService> services)
        {
            Services = services.ToList();
        }
    }
}
