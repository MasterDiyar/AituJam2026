using Godot;
using System;

public partial class UnitBody : Node2D
{
	[Export] public Sprite2D Body, Cloth, Head, Hat, Weapon, Secondary;
	[Export] public UnitResource uResource;
	public override void _Ready()
	{
		uResource.SetUnitBody(this);
	}

	private Tween at;

	public void OnAttack(float angle, float length, float time, float weaponAngleOffset = 0)
	{
		if (at != null && at.IsRunning())
			at.Kill();

		at = CreateTween();
    
		Vector2 direction = Vector2.FromAngle(angle);
		Vector2 targetPos = Weapon.Position + direction * length;
		Vector2 startPos = Weapon.Position;
		float startAngle = Weapon.Rotation;

		at.TweenProperty(Weapon, "position", targetPos, time / 2.0f)
			.SetTrans(Tween.TransitionType.Quad)
			.SetEase(Tween.EaseType.In);
		at.Parallel().TweenProperty(Weapon, "rotation", angle + weaponAngleOffset, time / 5.0f)
			.SetEase(Tween.EaseType.In);

		at.TweenProperty(Weapon, "position", startPos, time / 2.0f)
			.SetTrans(Tween.TransitionType.Quad)
			.SetEase(Tween.EaseType.Out);
		at.Parallel().TweenProperty(Weapon, "rotation", startAngle, time / 5.0f)
			.SetEase(Tween.EaseType.In);
	}
}
