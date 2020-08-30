namespace ActorsLib.DamageChain
{
    public class ShieldAffector : TimedDamageAffectorBase
    {
        public float DamageReduction;

        public override void Affect(DamageDescriptor damage)
        {
            damage.DamageAmount -= this.DamageReduction;
        }
    }
}