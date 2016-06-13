using System;
using System.IO;
using System.Reflection;

namespace Marvin.Core
{
    public static class Constants
    {
        public const string ModuleFolder = "modules";

        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetEntryAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
