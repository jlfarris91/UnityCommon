namespace ActorsLib.DamageChain
{
    public interface IDamageAffector
    {
        IDamageChain DamageChain { get; }
        void Affect(DamageDescriptor damage);
    }
}