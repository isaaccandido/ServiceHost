namespace Host.Domain.Interfaces
{
    public interface IService
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }

        bool IsRunning { get; set; }
        CancellationTokenSource CancellationToken { get; set; }

        void Start();
        void Stop();
    }
}
