using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace UserService.Mediator.Extensions
{
    internal static class TypeExtensions
    {
        public static IEnumerable<Type> GetAssignableTypes(this Type type)
        {
            return from assembly in GetAssemblies()
                from t in assembly.GetTypes()
                where t.GetInterfaces()
                    .Any(i => i.IsGenericType && type.IsAssignableFrom(i.GetGenericTypeDefinition()))
                select t;
        }
        public static IEnumerable<Type> GetTypesImplementingInterfaceWithSpecificArgument(this IEnumerable<Type> types, Type argumentType)
        {
            return from type in types
                   from @interface in type.GetInterfaces()
                   where @interface.GenericTypeArguments.Contains(argumentType)
                   select type;
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = new List<Assembly>();

            var directoryToScan = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;

            foreach (var assemblyFile in GetAssemblyFiles(directoryToScan))
            {
                var assembly = TryLoadAssembly(assemblyFile.FullName);
                if (assembly != null)
                    assemblies.Add(assembly);
            }

            return assemblies;
        }

        private static Assembly TryLoadAssembly(string path)
        {
            try
            {
                return Assembly.LoadFrom(path);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static List<FileInfo> GetAssemblyFiles(string directoryToScan)
        {
            var fileInfo = new List<FileInfo>();

            var baseDir = new DirectoryInfo(directoryToScan);
            foreach (var info in baseDir.GetFiles("*.dll", SearchOption.AllDirectories))
            {
                fileInfo.Add(info);
            }

            return fileInfo;
        }
    }
}