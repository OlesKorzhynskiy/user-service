using System;
using System.Collections.Generic;
using System.Linq;

namespace Mediator.Extensions
{
    internal static class TypeExtensions
    {
        public static IEnumerable<Type> GetAssignableTypes(this Type type)
        {
            return from assembly in AppDomain.CurrentDomain.GetAssemblies()
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

    }
}