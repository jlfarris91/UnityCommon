namespace ActorsLib.DamageChain
{
    using UnityEngine;

    [RequireComponent(typeof(DamageChain))]
    public abstract class DamageAffectorBase : MonoBehaviour, IDamageAffector
    {
        public DamageChain DamageChain
        {
            get { return this.GetComponent<DamageChain>(); }
        }

        IDamageChain IDamageAffector.DamageChain
        {
            get { return this.DamageChain; }
        }

        public abstract void Affect(DamageDescriptor damage);
    }
}