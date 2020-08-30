namespace ActorsLib.DamageChain
{
    using System.Collections.Generic;

    public interface IDamageChain
    {
        IEnumerable<IDamageAffector> Affectors { get; }

        void Affect(DamageDescriptor damage);
    }
}