using Godot;
using System;

public partial class Arena : Node2D
{
	[Export]public Button StartFight;
	[Export] Camera2D camera;
	
	bool isFightStarted = false;
	
	readonly Vector2 _cityPosition = new Vector2(0, 800);
	Tween _tween;
	public void ToggleMove(bool startBool)
	{
		isFightStarted = startBool;
		_tween?.Kill();
		_tween = CreateTween();
		
		_tween.TweenProperty(camera, "position", isFightStarted ? _cityPosition : Vector2.Zero , 2)
			.SetEase(Tween.EaseType.In);
	}

	public void OnAttack()
	{
		
	}

	public void OnCity()
	{
		
	}
	public void AfterInit()
	{
		StartFight.Pressed += () => ToggleMove(false);
	}
}
