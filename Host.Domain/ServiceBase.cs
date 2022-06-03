using Host.Domain.Interfaces;

namespace Host.Domain
{
    public abstract class ServiceBase : IService
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsRunning { get; set; }
        public CancellationTokenSource CancellationToken { get; set; }

        public abstract void Start();
        public void Stop() => CancellationToken.Cancel();
    }
}
