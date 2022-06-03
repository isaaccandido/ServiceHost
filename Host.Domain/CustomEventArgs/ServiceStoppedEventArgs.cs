using Host.Domain.Interfaces;

namespace Host.Domain.CustomEventArgs
{
    public class ServiceStoppedEventArgs : EventArgs
    {
        public IService Service { get; }

        public ServiceStoppedEventArgs(IService service)
        {
            this.Service = service;
        }
    }
}
