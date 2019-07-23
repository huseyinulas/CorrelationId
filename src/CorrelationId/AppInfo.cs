using System.Reflection;
using System;

namespace CorrelationId
{
    public class AppInfo
    {
        private static readonly AssemblyName _applicationVersion = Assembly.GetExecutingAssembly().GetName();

        public static string Name
        {
            get { return _applicationVersion.Name; }
        }

        public static Version Version
        {
            get { return _applicationVersion.Version; }
        }
    }
}
