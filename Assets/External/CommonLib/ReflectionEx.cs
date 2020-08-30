namespace CommonLib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ReflectionEx
    {
        public static bool HasAttribute<T>(this FieldInfo field) where T : Attribute
        {
            return field.GetCustomAttributes(typeof(T), true).Any();
        }

        public static T GetCustomAttribute<T>(this FieldInfo field) where T : Attribute
        {
            return field.GetCustomAttributes(typeof(T), true).Cast<T>().FirstOrDefault();
        }

        public static bool HasCustomAttribute<TAttribute>(this Type type)
        {
            return type.GetCustomAttributes(typeof(TAttribute), false).Any();
        }

        public static bool HasAttribute<T>(this MethodInfo method) where T : Attribute
        {
            return method.GetCustomAttributes(typeof(T), true).Any();
        }

        public static T GetCustomAttribute<T>(this MethodInfo method) where T : Attribute
        {
            return method.GetCustomAttributes(typeof(T), true).Cast<T>().FirstOrDefault();
        }

        public static bool HasAttribute<T>(this PropertyInfo property) where T : Attribute
        {
            return property.GetCustomAttributes(typeof(T), true).Any();
        }

        public static T GetCustomAttribute<T>(this PropertyInfo property) where T : Attribute
        {
            return property.GetCustomAttributes(typeof(T), true).Cast<T>().FirstOrDefault();
        }

        public static TAttribute GetCustomAttribute<TAttribute>(this Type type)
        {
            return (TAttribute)type.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault();
        }

        public static string GetFullName(this MethodBase method)
        {
            return string.Join(".", new[] { method.ReflectedType.FullName, method.Name }).Replace("+", ".");
        }

        public static string GetUnqualifiedName(this MethodBase method)
        {
            return string.Join(".", new[] { method.ReflectedType.Name, method.Name }).Replace("+", ".");
        }

        public static bool IsSameOrSubclassOf(this Type type, Type baseType)
        {
            return type == baseType || type.IsSubclassOf(baseType) || baseType.IsAssignableFrom(type);
        }

        public static IEnumerable<Type> FindTypes(Func<Type, bool> predicate)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] exportedTypes = null;
                try
                {
                    exportedTypes = assembly.GetExportedTypes();
                }
                catch (ReflectionTypeLoadException e)
                {
                    exportedTypes = e.Types;
                }

                if (exportedTypes != null)
                {
                    foreach (var type in exportedTypes)
                    {
                        if (predicate(type))
                            yield return type;
                    }
                }
            }
        }

        /// <summary>
        /// Gets a type from all loaded assemblies.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Type GetTypeFromAllAssemblies(string name)
        {
            Type easyType = Type.GetType(name);
            if (easyType != null)
            {
                return easyType;
            }

            return EnumerableEx.FirstOrDefault(AppDomain.CurrentDomain.GetAssemblies()
                    .Select(assembly => assembly.GetType(name)), type => type != null);
        }

        public static IEnumerable<TAttribute> GetAttributesFromAllAssemblies<TAttribute>() where TAttribute : Attribute
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetCustomAttributes(typeof (TAttribute), true).OfType<TAttribute>());
        }
    }
}
