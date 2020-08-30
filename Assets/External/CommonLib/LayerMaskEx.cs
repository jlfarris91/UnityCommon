namespace CommonLib
{
    using System.Linq;
    using UnityEngine;

    public static class LayerMaskEx
    {
        public static LayerMask Everything = ~0;
        public static LayerMask Default = 1 << 0;

        public static bool IsSet(this int mask, string layerMaskName)
        {
            int layer = 1 << LayerMask.NameToLayer(layerMaskName);
            return (mask & layer) != 0;
        }

        public static int Set(this int mask, string layerMaskName)
        {
            int layer = 1 << LayerMask.NameToLayer(layerMaskName);
            return mask | layer;
        }

        public static int Clear(this int mask, string layerMaskName)
        {
            int layer = 1 << LayerMask.NameToLayer(layerMaskName);
            return mask & ~layer;
        }

        public static int Invert(this int mask)
        {
            return ~mask;
        }

        public static LayerMask And(params LayerMask[] masks)
        {
            return masks.Aggregate((a, b) => a.value & b.value);
        }
    }
}
