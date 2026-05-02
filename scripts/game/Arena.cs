using Godot;
using System;
using AITUJAM2026.scripts.unit;

public partial class Arena : Node2D
{
	[Export]public Button StartFight;
	[Export] Camera2D camera;
	[Export] public GridContainer _mobsGrid;
	bool isFightStarted = false ,isPlayerWinner = false;
	private EnemyUi _enemyUi;

	public UnitBuilder[] Units;
	
	readonly Vector2 _cityPosition = new Vector2(0, 800);
	public float WinCoins = 2, WinFood = 2;
	private int Level = 1;
	Tween _tween;
	public void ToggleMove(bool startBool)
	{
		isFightStarted = startBool;
		_tween?.Kill();
		_tween = CreateTween();
		
		_tween.TweenProperty(camera, "position", isFightStarted ? _cityPosition : Vector2.Zero , 2)
			.SetEase(Tween.EaseType.In);
		StartFight.Visible = startBool;
		if (!startBool)
			OnAttack();
		else OnCity();
	}

	public void OnAttack()
	{
		_enemyUi.ToggleUi(true);
		WinCoins = Mathf.Pow(Level, 2);
		WinFood = Mathf.Pow(Level, 2)-Level*2;
		
	}

	public void OnCity()
	{
		_enemyUi.ToggleUi(false);
		foreach (var node in GameManager.Instance.Pausable.GetChildren())
			if (node is Unit uit) uit.QueueFree();
		
	}
	public void AfterInit()
	{
		StartFight.Pressed += () => ToggleMove(false);
		_enemyUi = GameManager.Instance.UI.GetNode<EnemyUi>("EnemyUI");
	}
}
