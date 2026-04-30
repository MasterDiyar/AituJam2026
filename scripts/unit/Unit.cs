using AITUJAM2026.scripts.interfaces;
using Godot;

namespace AITUJAM2026.scripts.unit;

public partial class Unit : CharacterBody2D, IDamagable
{
    [Export] public UnitBody Body;
    [Export] public AboutResource Stats;
    [Export] public Faction UnitFaction;

    
    public float MaxHp, MaxArmor, MaxAbsorbtion, DefaultDamage, KritChance, KritDamage, MaxSpeed;
    public float Hp, Armor, Absorbtion, Damage, Speed;

    public override void _Ready()
    {
        Stats.SetStats(this);
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public void Heal(float heal)
    {
        throw new System.NotImplementedException();
    }
}