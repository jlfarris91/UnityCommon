namespace ActorsLib.DamageChain
{
    public class InvincibleAffector : TimedDamageAffectorBase
    {
        public override void Affect(DamageDescriptor damage)
        {
            damage.DamageAmount = 0;
        }
    }
}