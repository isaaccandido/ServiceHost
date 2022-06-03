using Host.Domain;

namespace Host.Services.Tasks
{
    public class ThirdTest : ServiceBase
    {
        public ThirdTest() : base()
        {
            Id = Guid.NewGuid();
            Name = "TestBot the Third";
            Description = "Alla Turca";
        }

        public override void Start()
        {
            IsRunning = true;

            for (int i = 3; i > 0; i--)
            {
                Thread.Sleep(1000);
            }

            IsRunning = false;
        }
    }
}
