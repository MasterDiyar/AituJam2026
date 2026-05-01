using System;
using System.Linq;
using Godot;

namespace AITUJAM2026.scripts.unit;

public partial class Deck : Node
{
    [Export] public Faction team;
    [Export] public Godot.Collections.Array<Unit> units;
    [Export] public Godot.Collections.Array<UnitActions> actions;
    [Export] public float timePerAction;
    
    public int currentAction = 0;
    public Action<int> ActionChanged;

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
    }

    private void TimerOnTimeout()
    {
        currentAction = (currentAction + 1) % actions.Count;
        SetAction(actions[currentAction]);
        ActionChanged?.Invoke(currentAction);
    }

    public void SetAction(UnitActions action)
    {
        units = new Godot.Collections.Array<Unit>(units.Where(IsInstanceValid));
        foreach (var unit in units)
            unit.Controller.Act(action);
        
    }
}