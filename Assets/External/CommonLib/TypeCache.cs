namespace CommonLib
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class TypeCache
    {
        private static readonly Dictionary<string, Type> Cache; 

        static TypeCache()
        {
            TypeCache.Cache = new Dictionary<string, Type>();
        }

        public static Type GetType(string typeName, bool force = false)
        {
            Type type;

            if (TypeCache.Cache.TryGetValue(typeName, out type))
            {
                return type;
            }

            type = ReflectionEx.GetTypeFromAllAssemblies(typeName);
            if (type != null)
            {
                TypeCache.Cache.Add(typeName, type);
            }

            return type;
        }
    }
}