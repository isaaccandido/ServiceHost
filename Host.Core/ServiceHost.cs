using Host.Core.Helpers;
using Host.Domain.CustomEventArgs;
using Host.Domain.Enums;
using Host.Domain.Interfaces;

namespace Service.Host
{
    public class ServiceHost
    {
        public event EventHandler<BootEventArgs> HostStarted;
        public event EventHandler<HaltEventArgs> HostStopped;

        public event EventHandler<ServiceStartedEventArgs> ServiceStarted;
        public event EventHandler<ServiceStoppedEventArgs> ServiceStopped;

        public event EventHandler<LoadedServicesListEventArgs> LoadedServiceList;
        public event EventHandler<LoadedAddonsListEventArgs> LoadedAddonList;

        public event EventHandler<StoppedServicesListEventArgs> StoppedServiceList;
        

        public bool IsRunning { get; private set; }

        private List<IService> _services = new();
        private List<IAddon> _addons = new();

        private readonly CancellationTokenSource _cancellationToken = new();

        public ServiceHost()
        {
            LoadAddons();
            InjectCancellationToken();
        }

        public async void Run()
        {
            IsRunning = true;
            HostStarted?.Invoke(this, new BootEventArgs());

            await ServiceRunner();

            HostStopped?.Invoke(this, new HaltEventArgs());
            IsRunning = false;
        }

        private void LoadAddons()
        {
            {
                _services = InitHelper.GetInstancesFromType<IService>(AssemblyItem.Services).ToList();
                
                if (_services.Count is 0) throw new Exception("No services were found to run.");

                LoadedServiceList?.Invoke(this, new LoadedServicesListEventArgs(_services));
            }
            {
                _addons = InitHelper.GetInstancesFromType<IAddon>(AssemblyItem.Services).ToList();

                if (_addons.Count is 0) throw new Exception("No services were found to run.");

                LoadedAddonList?.Invoke(this, new LoadedAddonsListEventArgs(_addons));
            }
        }
        private void InjectCancellationToken()
        {
            _services.ForEach(service => service.CancellationToken = _cancellationToken);
        }
        private Task ServiceRunner()
        {
            _services.ForEach(service => Task.Run(() =>
            {
                ServiceStarted?.Invoke(this, new ServiceStartedEventArgs(service));
                service.Start();
            }));

            while (!_services.Any(service => service.IsRunning)) continue;

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