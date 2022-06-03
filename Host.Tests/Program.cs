using Host.Tests;
using Service.Host;

var host = new ServiceHost();
host.HostStarted += DisplayHelper.EventCenter;
host.HostStopped += DisplayHelper.EventCenter;
host.LoadedServiceList += DisplayHelper.EventCenter;
host.ServiceStarted += DisplayHelper.EventCenter;
host.ServiceStopped += DisplayHelper.EventCenter;
host.StoppedServiceList += DisplayHelper.EventCenter;

try
{
    host.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Error! {ex.Message}");
}
finally
{
    Console.WriteLine("END!");
}

