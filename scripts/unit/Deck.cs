using Godot;

namespace AITUJAM2026.scripts.unit;

public partial class Deck : Node
{
    [Export] public Faction team;
    [Export] public Godot.Collections.Array<Unit> units;
    

    public override void _Ready()
    {
        foreach (var unit in units)
        {
            unit.UnitFaction = team;
        }
    }

    public void SetAction(UnitActions action)
    {
        foreach (var unit in units)
        {
            unit.Controller.Act(action);
        }
    }
}