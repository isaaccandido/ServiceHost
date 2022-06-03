using Host.Domain.Enums;
using System.ComponentModel;
using System.Reflection;

namespace Host.Core.Helpers
{
    internal class InitHelper
    {
        /// <summary>
        /// Loads instances from a designed interface type found in a defined assembly and instantiate them through a default constructor.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="assembly">Sets the assembly item from which to load definitions.</param>
        /// <returns>List of found and instantied objects.</returns>
        internal static IEnumerable<T> GetInstancesFromType<T>(AssemblyItem assembly)
        {
            return Assembly.Load((assembly.GetType()
                                          .GetField(assembly.ToString())
                                          .GetCustomAttributes(typeof(DescriptionAttribute), false) as IEnumerable<DescriptionAttribute>
                                 )?.First()
                                   .Description)
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
