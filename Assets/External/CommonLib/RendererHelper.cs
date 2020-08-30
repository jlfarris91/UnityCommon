namespace CommonLib
{
    using UnityEngine;

    public static class RendererHelper
    {
        public static void SetColor<T>(Color color, params T[] renderers) where T : Renderer
        {
            var propertyBlock = new MaterialPropertyBlock();
            foreach (T r in renderers)
            {
                r.GetPropertyBlock(propertyBlock);
                propertyBlock.SetColor("_Color", color);
                r.SetPropertyBlock(propertyBlock);
            }
        }
    }
}
