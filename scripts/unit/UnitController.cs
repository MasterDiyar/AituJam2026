using Godot;

namespace AITUJAM2026.scripts.unit;

public partial class UnitController : Node2D
{
    public UnitActions UnitAction;

    public void Act(UnitActions action)
    {
        UnitAction = action;

        switch (UnitAction)
        {
            case UnitActions.GoForward:
                
                break;
        }
    }

    public void Go(Vector2 facing)
    {
        
    }
}