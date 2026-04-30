using Godot;

namespace AITUJAM2026.scripts.unit;

public partial class UnitController : Node2D
{
    public UnitActions UnitAction;
    private Unit Parent;

    public override void _Ready()
    {
        Parent = GetParent<Unit>();
    }

    public void Act(UnitActions action)
    {
        UnitAction = action;

        switch (UnitAction)
        {
            case UnitActions.GoForward: Go(Vector2.Up); break;
            case UnitActions.GoBackward: Go(Vector2.Down); break;
            case UnitActions.Attack : Attack(); break;
        }
    }

    public void Go(Vector2 facing)
    {
        _absolut = (Parent.UnitFaction == Faction.Player) ? 1 : -1; 
        _facing = facing;
    }

    private int _absolut = 1;
    float _acceleration = 10.0f;
    private Vector2 _facing = Vector2.Up;

    public override void _PhysicsProcess(double delta)
    {
        float dt = (float)delta;
        bool isMoving = UnitAction is UnitActions.GoForward or UnitActions.GoBackward;

        if (isMoving && _facing != Vector2.Zero) {
            Vector2 targetVelocity = _facing * _absolut * Parent.MaxSpeed;
            Parent.Velocity = Parent.Velocity.Lerp(targetVelocity, _acceleration * dt);
        }else Parent.Velocity = Vector2.Zero;
        Parent.MoveAndSlide();
    }

    public void Attack()
    {
        
    }
}