using Godot;

namespace AITUJAM2026.scripts.unit;

public partial class UnitController : Node2D
{
    [Export] public float SearchRadius = 200f;
    [Export]public Area2D Eyes;
    public UnitActions UnitAction;
    private Unit Parent, closestEnemy;
    

    public override void _Ready()
    {
        Parent = GetParent<Unit>();
        ((CircleShape2D)Eyes.GetChild<CollisionShape2D>(0).Shape).Radius = SearchRadius;
        Eyes.BodyEntered += OnEyeEntered;
    }

    private void OnEyeEntered(Node2D body)
    {
        if (body is not Unit unit) return;
        if (unit.UnitFaction == Parent.UnitFaction) return;
            
        closestEnemy = unit;
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
    private Vector2 _facing = Vector2.Up, targetVelocity;

    public override void _PhysicsProcess(double delta)
    {
        var dt = (float)delta;

        switch (UnitAction) {
            case UnitActions.GoForward or UnitActions.GoBackward when _facing != Vector2.Zero:
                targetVelocity = _facing * _absolut * Parent.MaxSpeed;
                Parent.Velocity = Parent.Velocity.Lerp(targetVelocity, _acceleration * dt);
                break;
            case UnitActions.GoForward or UnitActions.GoBackward:
                Parent.Velocity = Parent.Velocity.Lerp(Vector2.Zero, _acceleration * dt);
                break;
            case UnitActions.Attack when closestEnemy != null && IsInstanceValid(closestEnemy): {
                float distanceToEnemy = Parent.GlobalPosition.DistanceTo(closestEnemy.GlobalPosition);
                float attackDistance = Parent.Weapon.WeaponInstance.AttackDistance;

                if (distanceToEnemy > attackDistance) {
                    Vector2 direction = (closestEnemy.GlobalPosition - Parent.GlobalPosition).Normalized();
                    targetVelocity = direction * Parent.MaxSpeed;
                    Parent.Velocity = Parent.Velocity.Lerp(targetVelocity, _acceleration * dt);
                }else {
                    Parent.Velocity = Parent.Velocity.Lerp(Vector2.Zero, _acceleration * dt);
                    Parent.Weapon.Execute(closestEnemy);
                }

                break;
            }
            case UnitActions.Attack:
                Parent.Velocity = Parent.Velocity.Lerp(Vector2.Zero, _acceleration * dt);
                break;
            default:
                Parent.Velocity = Vector2.Zero;
                break;
        }

        Parent.MoveAndSlide();
    }

    public void Attack()
    {
        
    }
}