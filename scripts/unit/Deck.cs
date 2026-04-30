using Godot;

namespace AITUJAM2026.scripts.unit;

public partial class Deck : Node
{
    [Export] public Faction team;
    [Export] public Godot.Collections.Array<Unit> units;
    [Export] public Godot.Collections.Array<UnitActions> actions;
    [Export] public float timePerAction;
    
    int currentAction = 0;

    public override void _Ready()
    {
        foreach (var unit in units)
        {
            unit.UnitFaction = team;
        }
        
        Timer timer = new Timer();
        AddChild(timer);
        timer.WaitTime = timePerAction;
        timer.Start();
        timer.Timeout += TimerOnTimeout;
        CallDeferred(nameof(TimerOnTimeout));
    }

    private void TimerOnTimeout()
    {
        currentAction = (currentAction + 1) % actions.Count;
        SetAction(actions[currentAction]);
        
    }

    public void SetAction(UnitActions action)
    {
        foreach (var unit in units)
        {
            unit.Controller.Act(action);
        }
    }
}