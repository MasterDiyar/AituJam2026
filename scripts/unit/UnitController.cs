using Godot;
using AITUJAM2026.scripts.unit;

namespace AITUJAM2026.scripts.unit;

public partial class UnitController : Node2D
{
    [Export] public float SearchRadius = 200f;
    [Export] public Area2D Eyes;
    public UnitActions UnitAction { get; private set; }
    private Unit Parent, closestEnemy;

    private int _absolut = 1;
    private float _acceleration = 10.0f;
    private Vector2 _facing = Vector2.Up;
    private Vector2 _targetVelocity;

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

    public void Act(UnitActions newAction)
    {
        if (UnitAction == UnitActions.Defend && newAction != UnitActions.Defend)
            Parent.Absorbtion -= 4f;
        

        if (newAction == UnitActions.Defend && UnitAction != UnitActions.Defend)
            Parent.Absorbtion += 4f; 
        

        UnitAction = newAction;

        switch (UnitAction)
        {
            case UnitActions.GoForward:
            case UnitActions.Rush: 
                Go(Vector2.Up); 
                break;
            case UnitActions.GoBackward: 
                Go(Vector2.Down); 
                break;
        }
    }

    public void Go(Vector2 facing)
    {
        _absolut = (Parent.UnitFaction == Faction.Player) ? 1 : -1; 
        _facing = facing;
    }

    public override void _PhysicsProcess(double delta)
    {
        var dt = (float)delta;
        if (closestEnemy != null && !IsInstanceValid(closestEnemy))
            closestEnemy = null;
        

        switch (UnitAction) {
            case UnitActions.GoForward:
            case UnitActions.GoBackward:
                ProcessMovement(dt);
                break;
            case UnitActions.Attack:
                ProcessAttack(dt);
                break;
            case UnitActions.Rush:
                ProcessRush(dt);
                break;
            case UnitActions.Heal:
                ProcessHeal(dt, 10f); 
                break;
            case UnitActions.Idle:
                ProcessHeal(dt, 2.5f); 
                break;
            case UnitActions.Defend:
                ProcessDefend(dt);
                break;
            default:
                StopMoving(dt);
                break;
        }

        Parent.MoveAndSlide();
    }


    private void ProcessMovement(float dt)
    {
        if (_facing != Vector2.Zero) {
            _targetVelocity = _facing * _absolut * Parent.MaxSpeed;
            Parent.Velocity = Parent.Velocity.Lerp(_targetVelocity, _acceleration * dt);
        }
        else 
            StopMoving(dt);
        
    }

    private void ProcessAttack(float dt)
    {
        if (closestEnemy != null && IsInstanceValid(closestEnemy)) {
            float distanceToEnemy = Parent.GlobalPosition.DistanceTo(closestEnemy.GlobalPosition);
            float attackDistance = Parent.Weapon.WeaponInstance.AttackDistance;

            if (distanceToEnemy > attackDistance) {
                Vector2 direction = (closestEnemy.GlobalPosition - Parent.GlobalPosition).Normalized();
                _targetVelocity = direction * Parent.MaxSpeed;
                Parent.Velocity = Parent.Velocity.Lerp(_targetVelocity, _acceleration * dt);
            }else {
                StopMoving(dt);
                Parent.Weapon.Execute(closestEnemy);
            }
        }
        else
            StopMoving(dt);
        
    }

    private void ProcessRush(float dt)
    {
        if (closestEnemy != null && IsInstanceValid(closestEnemy))
            ProcessAttack(dt);
        else ProcessMovement(dt);
        
    }

    private void ProcessHeal(float dt, float healPerSecond)
    {
        StopMoving(dt);
        Parent.Heal(healPerSecond * dt); 
    }

    private void ProcessDefend(float dt)
    {
        StopMoving(dt);
    }

    private void StopMoving(float dt)
    {
        Parent.Velocity = Parent.Velocity.Lerp(Vector2.Zero, _acceleration * dt);
    }
}