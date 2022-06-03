using Host.Core.Helpers;
using Host.Domain.CustomEventArgs;
using Host.Domain.Interfaces;

namespace Service.Host
{
    public class ServiceHost
    {
        public event EventHandler<BootEventArgs> HostStarted;
        public event EventHandler<HaltEventArgs> HostStopped;

        public event EventHandler<ServiceStartedEventArgs> ServiceStarted;
        public event EventHandler<ServiceStoppedEventArgs> ServiceStopped;
        public event EventHandler<StoppedServicesListEventArgs> StoppedServiceList;
        public event EventHandler<LoadedServicesListEventArgs> LoadedServiceList;

        public bool IsRunning { get; set; }

        private readonly List<IService> _services;
        private readonly CancellationTokenSource _cancellationToken;

        public ServiceHost()
        {
            _cancellationToken = new();
            _services = new();

            _services = InitHelper.GetServiceInstances<IService>()
                                  .ToList();

            _services.ForEach(service => service.CancellationToken = _cancellationToken);

            if (_services.Count is 0) throw new Exception("No services were found to run.");
        }

        public async void Run()
        {
            IsRunning = true;

            HostStarted?.Invoke(this, new BootEventArgs());
            LoadedServiceList?.Invoke(this, new LoadedServicesListEventArgs(_services));

            await ServiceRunner();

            HostStopped?.Invoke(this, new HaltEventArgs());

            IsRunning = false;
        }

        private Task ServiceRunner()
        {
            _services.ForEach(service => Task.Run(() =>
            {
                ServiceStarted?.Invoke(this, new ServiceStartedEventArgs(service));
                service.Start();
            }));

            while(!_services.Any(service => service.IsRunning)) continue;

            ServiceStatusListener();

            return Task.CompletedTask;
        }
        private void ServiceStatusListener()
        {
            List<IService> stopped = new();
            List<IService> notified = new();

            bool _anyServiceRunning = true;

            while (_anyServiceRunning)
            {
                _anyServiceRunning = _services.Any(service => service.IsRunning);

                foreach (var service in _services.Where(service => !service.IsRunning))
                {
                    if (!stopped.Contains(service)) stopped.Add(service);
                }

                foreach (var service in stopped)
                {
                    if (!notified.Contains(service))
                    {
                        ServiceStopped?.Invoke(this, new ServiceStoppedEventArgs(service));
                        notified.Add(service);
                    }
                }
            }

            StoppedServiceList?.Invoke(this, new StoppedServicesListEventArgs(_services));
        }
    }
}
