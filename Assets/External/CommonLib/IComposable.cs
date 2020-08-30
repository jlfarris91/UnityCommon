namespace CommonLib
{
    using UnityEngine;

    public interface IComposable
    {
        T GetComponent<T>() where T : Component;
    }
}
