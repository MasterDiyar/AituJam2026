
using AITUJAM2026.scripts.unit;
using Godot;

[GlobalClass]
public partial class AboutResource : Resource
{
    [Export] public float MaxHp, MaxArmor, MaxAbsorbtion, DefaultDamage, KritChance, KritDamage, MaxSpeed;

    public void SetStats(Unit unit)
    {
        unit.MaxHp = MaxHp;
        unit.MaxArmor = MaxArmor;
        unit.MaxAbsorbtion = MaxAbsorbtion;
        unit.DefaultDamage = DefaultDamage;
        unit.KritChance = KritChance;
        unit.KritDamage = KritDamage;
        unit.Hp = MaxHp;
        unit.Armor = MaxArmor;
        unit.Absorbtion = MaxAbsorbtion;
        unit.Speed = MaxSpeed;
        unit.MaxSpeed = MaxSpeed;
    }
}