using System.ComponentModel;

namespace Host.Domain.Enums
{
    /// <summary>
    /// Items from this project. Add a new Assembly here if new projects are added.
    /// </summary>
    public enum AssemblyItem
    {
        [Description("Host.Core")]
        Core,

        [Description("Host.Domain")]
        Domain,

        [Description("Host.Services")]
        Services
    }
}
