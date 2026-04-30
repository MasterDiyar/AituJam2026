using Godot;
[GlobalClass]
public partial class WeaponResource : Resource
{
    [Export] public BulletResource Bullet;
    
    [Export] public float Damage;
    [Export] public float AttackSpeed;
    [Export] public int Count;
    [Export] public bool isClose;
    [Export] public float AttackDistance;
    [Export] public float BetWeenAngle;

}