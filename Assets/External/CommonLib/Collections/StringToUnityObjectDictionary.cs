namespace CommonLib.Collections
{
    using System;
    using Object = UnityEngine.Object;

    [Serializable]
    public class StringToUnityObjectDictionary : SerializableDictionary<string, Object>
    {
    }
}