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
                var type = typeof(TEventArgs);

                if (type.Equals(typeof(LoadedServicesListEventArgs)))
                {
                    var eventArg = e as LoadedServicesListEventArgs;

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Services Loaded:");
                    eventArg.Services.ForEach(service => Console.WriteLine($"> {service.Name}"));
                    Console.WriteLine();

                    Console.ResetColor();
                }
                else if (type.Equals(typeof(ServiceStartedEventArgs)))
                {
                    var eventArg = e as ServiceStartedEventArgs;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"> Service Started: {eventArg.Service.Name}");
                    Console.ResetColor();
                }
                else if (type.Equals(typeof(ServiceStoppedEventArgs)))
                {
                    var eventArg = e as ServiceStoppedEventArgs;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"> Service Stopped: {eventArg.Service.Name}");
                    Console.ResetColor();
                }
                else if (type.Equals(typeof(StoppedServicesListEventArgs)))
                {
                    var eventArg = e as StoppedServicesListEventArgs;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine();
                    Console.WriteLine("Stopped services:");
                    eventArg.Services.ForEach(service => Console.WriteLine($"> {service.Name}"));
                    Console.WriteLine();
                    Console.ResetColor();
                }
                else if (type.Equals(typeof(HaltEventArgs)))
                {
                    Console.ResetColor();
                    Console.WriteLine("Finished ServiceHost software...");
                }
                else if (type.Equals(typeof(BootEventArgs)))
                {
                    Console.ResetColor();
                    Console.WriteLine("Booting up ServiceHost software...");
                }
            }
        }
    }
}
