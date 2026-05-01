using Godot;
using System;
using AITUJAM2026.scripts.interfaces;
using AITUJAM2026.scripts.unit;

public partial class Bullet : Area2D
{
	public Faction hozyain;
	public BulletResource BulletResource;
	public float Speed, Damage, LifeTime, Consume, SpawnAngle;
	[Export] public Sprite2D BulletSprite;
	public override void _Ready()
	{
		BulletResource.SetBullet(this);
		BodyEntered += OnBodyEntered;
	}

	public override void _Process(double delta)
	{
		var dt = (float)delta;
		var dir = Vector2.FromAngle(Rotation - SpawnAngle).Normalized();
		Position += dir * Speed * dt;
		LifeTime -= dt;
		if (LifeTime <= 0)
			QueueFree();
	}

	public void OnBodyEntered(Node2D body)
	{
		if (body is IDamagable d)
		{
			d.TakeDamage(Damage);
		}
	}
}
