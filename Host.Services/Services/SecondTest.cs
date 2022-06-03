using Host.Domain;

namespace Host.Services.Services
{
    public class SecondTest : ServiceBase
    {
        public SecondTest() : base()
        {
            Id = Guid.NewGuid();
            Name = "TestBot the Second";
            Description = "Alla Turca";
        }

        public override void Start()
        {
            IsRunning = true;

            for (int i = 17; i > 0; i--)
            {
                Thread.Sleep(1000);
            }

            IsRunning = false;
        }
    }
}
