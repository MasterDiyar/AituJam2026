
using Godot;

[GlobalClass]
public partial class BulletResource :  Resource
{
    [Export] public PackedScene BulletResourceScene;

    [Export] public Texture2D Icon;
    [Export] public float Speed;
    [Export] public float Damage, LifeTime, Consume, SpawnAngle;

    public void SetBullet(Bullet bullet)
    {
        bullet.Speed = Speed;
        bullet.Damage += Damage;
        bullet.LifeTime = LifeTime;
        bullet.Consume = Consume;
        bullet.Rotation += SpawnAngle;
        bullet.BulletSprite.Texture = Icon;
        bullet.SpawnAngle = SpawnAngle;
    }
}