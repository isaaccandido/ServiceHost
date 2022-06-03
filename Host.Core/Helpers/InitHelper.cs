using System.Reflection;

namespace Host.Core.Helpers
{
    internal class InitHelper
    {
        internal static IEnumerable<T> GetServiceInstances<T>()
        {
            return Assembly.Load("Host.Services")
                           .GetTypes()
                           .Where(type => type.IsClass)
                           .Where(type => !type.IsAbstract)
                           .Where(type => typeof(T)
                           .IsAssignableFrom(type))
                           .Select(type => (T)Activator.CreateInstance(type))
                           .ToList();
        }
    }
}
