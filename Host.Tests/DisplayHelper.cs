using Host.Domain.CustomEventArgs;

namespace Host.Tests
{
    internal class DisplayHelper
    {
        private static object lockObj = new object();

        public static void EventCenter<TEventArgs>(object sender, TEventArgs e)
        {
            lock (lockObj)
            {
                if (e is LoadedServicesListEventArgs lslArgs)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Services Loaded:");
                    lslArgs.Services.ForEach(service => Console.WriteLine($"> {service.Name}"));
                    Console.WriteLine();

                    Console.ResetColor();
                }
                else if (e is ServiceStartedEventArgs ssArgs)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"> Service Started: {ssArgs.Service.Name}");
                    Console.ResetColor();
                }
                else if (e is ServiceStoppedEventArgs stArgs)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"> Service Stopped: {stArgs.Service.Name}");
                    Console.ResetColor();
                }
                else if (e is StoppedServicesListEventArgs stsArgs)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine();
                    Console.WriteLine("Stopped services:");
                    stsArgs.Services.ForEach(service => Console.WriteLine($"> {service.Name}"));
                    Console.WriteLine();
                    Console.ResetColor();
                }
                else if (e is HaltEventArgs hArgs)
                {
                    Console.ResetColor();
                    Console.WriteLine("Finished ServiceHost software...");
                }
                else if (e is BootEventArgs bArgs)
                {
                    Console.ResetColor();
                    Console.WriteLine("Booting up ServiceHost software...");
                }
            }
        }
    }
}
