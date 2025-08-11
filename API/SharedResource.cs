using Microsoft.Extensions.Localization;
using System.Reflection;

namespace API
{
    public class SharedResource
    {
    }

    public interface IResourceLocalizer
    {
        string Localize(string key);
    }

    public class ResourceLocalizer : IResourceLocalizer
    {
        private readonly IStringLocalizer _localizer;

        public ResourceLocalizer(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.Assembly.FullName!);
            _localizer = factory.Create("Messages", assemblyName.Name!);
        }

        public string Localize(string key)
        {
            return _localizer[key];
        }
    }
}
