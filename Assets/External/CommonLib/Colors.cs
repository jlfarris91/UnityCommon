namespace CommonLib
{
    using UnityEngine;

    public static class Colors
    {
        public static readonly Color cornflowerBlue = new Color(0.13f, 0.4f, 0.9f);
    }

    public static class ColorsEx
    {
        public static Color MoveTowards(Color current, Color target, float maxDelta)
        {
            current.r = Mathf.MoveTowards(current.r, target.r, maxDelta);
            current.g = Mathf.MoveTowards(current.g, target.g, maxDelta);
            current.b = Mathf.MoveTowards(current.b, target.b, maxDelta);
            current.a = Mathf.MoveTowards(current.a, target.a, maxDelta);
            return current;
        }
    }
}