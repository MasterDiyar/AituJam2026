using Godot;
[GlobalClass]
public partial class WeaponResource : Resource
{
    [Export] private BulletResource Bullet;
    
    [Export] float Damage;
    [Export] float AttackSpeed;
    [Export] int Count;
    [Export] private bool isClose;

}