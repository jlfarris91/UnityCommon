namespace ActorsLib.DamageChain
{
    using UnityEngine;

    public abstract class TimedDamageAffectorBase : DamageAffectorBase
    {
        public float Duration;

        private float elapsedTime;

        private void OnEnable()
        {
            this.elapsedTime = 0.0f;
        }

        private void Update()
        {
            this.elapsedTime += Time.deltaTime;

            if (this.elapsedTime >= this.Duration)
            {
                this.DamageChain.RemoveAffector(this);
            }
        }
    }

    public static class TimedDamageAffectorDamageChainEx
    {
        public static TAffector AddAffector<TAffector>(this DamageChain damageChain, float duration)
            where TAffector : TimedDamageAffectorBase
        {
            var affector = damageChain.AddAffector<TAffector>();
            affector.Duration = duration;
            return affector;
        }
    }
}