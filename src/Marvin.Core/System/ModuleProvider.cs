using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Marvin.Core.Module;

namespace Marvin.Core.System
{
    internal static class ModuleProvider
    {
        /// <summary>
        /// Load all modules exporting IModule found in the folder Constants.AssemblyDirectory + Constants.ModuleFolder
        /// </summary>
        public static List<IModule> LoadModules()
        {
            var conventions = new ConventionBuilder();
            conventions.ForTypesDerivedFrom<IModule>().Export<IModule>().Shared();
            var configuration = WithAssembliesInPath(new ContainerConfiguration(), Path.Combine(Constants.AssemblyDirectory, Constants.ModuleFolder), conventions);

            List<IModule> modules;
            using (var container = configuration.CreateContainer())
            {
                modules = container.GetExports<IModule>().ToList();
                foreach (var module in modules)
                {
                    Console.WriteLine($"Module loaded: {module.GetName()}");
                }
            }

            return modules;
        }

        private static ContainerConfiguration WithAssembliesInPath(ContainerConfiguration configuration,
            string path, AttributedModelProvider conventions,
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var assemblies = new List<Assembly>();
            var assemblieFiles = Directory.GetFiles(path, "*.dll", searchOption);
            foreach (var assemblyFile in assemblieFiles)
            {
                try
                {
                    var ctx = new MarvinAssemblyLoadContext();
                    AssemblyLoadContext.InitializeDefaultContext(ctx);

                    var name = AssemblyLoadContext.GetAssemblyName(assemblyFile);
                    var asm = Assembly.Load(name);

                    assemblies.Add(asm);
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine("Error loading module: " + e.Message + " from: " + path);
                }
            }

            return configuration.WithAssemblies(assemblies, conventions);
        }

        public class MarvinAssemblyLoadContext : AssemblyLoadContext
        {
            protected override Assembly Load(AssemblyName assemblyName)
            {
                var fileLocation = Path.Combine(Constants.AssemblyDirectory, Constants.ModuleFolder, assemblyName.Name + ".dll");
                return LoadFromAssemblyPath(fileLocation);
            }
        }
    }
}
