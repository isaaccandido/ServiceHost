using Host.Domain.Interfaces;

namespace Host.Domain.CustomEventArgs
{
    public class ServiceStartedEventArgs : EventArgs
    {
        public IService Service { get; set; }

        public ServiceStartedEventArgs(IService service)
        {
            Service = service;  
        }
    }
}
