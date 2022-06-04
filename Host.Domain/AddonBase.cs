using Host.Domain.Interfaces;

namespace Host.Domain
{
    public abstract class AddonBase : IAddon
    {
        public virtual void Run() { }
    }
}
