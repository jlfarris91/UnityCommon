namespace ActorsLib.DamageChain
{
    using System.Collections.Generic;
    using System.Linq;
    using CommonLib;
    using UnityEngine;

    public class DamageChain : MonoBehaviour, IDamageChain
    {
        public IEnumerable<IDamageAffector> Affectors
        {
            get { return this.GetComponents<IDamageAffector>(); }
        }

        public TAffector AddAffector<TAffector>()
            where TAffector : Component, IDamageAffector
        {
            return this.gameObject.AddComponent<TAffector>();
        }

        public void RemoveAffectors<TAffector>()
            where TAffector : Component, IDamageAffector
        {
            TAffector[] affectors = this.GetComponents<TAffector>();
            foreach (TAffector affector in affectors)
            {
                this.RemoveAffector(affector);
            }
        }

        public void RemoveAffector<TAffector>(TAffector affector)
            where TAffector : Component, IDamageAffector
        {
            ThrowIf.ArgumentIsNull(affector, "affector");
            ThrowIf.False((DamageChain)affector.DamageChain == this, "Trying to remove a affector from another chain.");
            Object.Destroy(affector);
        }

        public void Affect(DamageDescriptor damage)
        {
            List<IDamageAffector> affectors = this.Affectors.ToList();

            affectors.Foreach(_ => _.Affect(damage));
        }
    }
}