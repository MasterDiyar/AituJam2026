using AITUJAM2026.scripts.unit;
using Godot;

[GlobalClass]
public partial class UnitBuilder : Resource
{
    [Export] UnitResource unitBody;

    [Export] private WeaponResource weapon;
    [Export] private AboutResource stats;

    [Export] private int count = 1;
    

    PackedScene unitScene = GD.Load<PackedScene>("res://scenes/unit/unit.tscn");

    public Unit Setup(Faction unitFaction)
    {
        var unit = unitScene.Instantiate<Unit>();
        unit.Body.uResource = unitBody;
        unit.Stats = stats;
        unit.UnitFaction = unitFaction;
        
        unit.Stats.SetStats(unit);

        return unit;
    }

    public UnitBuilder(UnitResource unit, WeaponResource weapon, AboutResource stats, int count)
    {
        unitBody = unit;
        this.weapon = weapon;
        this.stats = stats;
        this.count = count;
    }
}