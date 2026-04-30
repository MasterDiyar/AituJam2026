using AITUJAM2026.scripts.unit;

namespace AITUJAM2026.scripts.interfaces;

public interface IDamagable
{
    void TakeDamage(float damage);
    Faction UnitFaction { get; }
    
    void Heal(float heal);
}