namespace CommonLib
{
    using System.Linq;
    using UnityEngine;

    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (SingletonScriptableObject<T>.instance == null)
                {
                    SingletonScriptableObject<T>.instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault() ??
                                                            SingletonScriptableObject<T>.CreateInstance<T>();
                }

                return SingletonScriptableObject<T>.instance;
            }
        }
    }
}
