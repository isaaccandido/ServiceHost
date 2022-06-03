using Host.Domain;

namespace Host.Services.Tasks
{
    public class Test : ServiceBase
    {
        public Test() : base()
        {
            Id = Guid.NewGuid();
            Name = "TestBot";
            Description = "Alla Turca";
        }

        public override void Start()
        {
            IsRunning = true;

            for (int i = 13; i > 0; i--)
            {
                Thread.Sleep(1000);
            }

            IsRunning = false;
        }
    }
}
