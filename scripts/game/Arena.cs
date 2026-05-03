using Godot;
using System;
using System.Collections.Generic;
using AITUJAM2026.scripts.unit;
using Godot.Collections;

public partial class Arena : Node2D
{
	readonly Vector2 _cityPosition = new Vector2(0, 800), _mobSpawnPosition = new (800, 150), _playerSpawnPosition = new (700, 550);
	
	[Export] public Button StartFight;
	[Export] public Camera2D camera;
	[Export] public GridContainer _mobsGrid;

	public  List<Unit> EnemyUnits = [], PlayerUnits = [];
	public  List<UnitBuilder> FullTimeUnits = [], OneTimeUnits = [];

	private EnemyUi _enemyUi;
	private Deck _testDeck;
	private Tween _tween;
	
	private bool  isFightStarted = false ,isPlayerWinner = false;
	private int   Level = 1;
	private float WinCoins = 2, WinFood = 2;
	
	
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
		string  randomName = _enemyUi.Names[GD.RandRange(0, _enemyUi.Names.Length-1)], 
				randomLink = _enemyUi.Links[GD.RandRange(0, _enemyUi.Links.Length-1)];
		var randomActions = Decks.PreMadeActions[GD.RandRange(0, Decks.PreMadeActions.Length-1)];
		
		_enemyUi.UpdateDisplay(randomName, randomLink, randomActions);
		_enemyUi.ToggleUi(true);
		
		SpawnEnemyMobs(Level);
		SpawnPlayerUnits();
		
		WinCoins = Mathf.Pow(Level, 2);
		WinFood = Mathf.Pow(Level, 2)-Level*2;
	}

	public void OnCity()
	{
		_enemyUi.ToggleUi(false);
		foreach (var node in GameManager.Instance.Pausable.GetChildren())
			if (node is Unit uit) uit.QueueFree();
	}

	public void OnWin()
	{
		Level++;
		GameManager.Instance.Money += WinCoins;
		GameManager.Instance.Food += WinFood;
		OneTimeUnits = [];
		RemoveUnitFromArmy();
		ToggleMove(true);
	}

	public void OnLoose()
	{
		
		RemoveUnitFromArmy();
		ToggleMove(true);
	}
	public void AfterInit()
	{
		StartFight.Pressed += () => ToggleMove(false);
		_enemyUi = GameManager.Instance.UI.GetNode<EnemyUi>("EnemyUI");
		_testDeck = _enemyUi.GetNode<Deck>("TestControl");	
		AddUnitToArmy(Decks.kopie_ton, true);
	}

	public void SpawnEnemyMobs(int level)
	{
		_enemyUi.WorkingDeck.units.Clear();
		var deck = Decks.PreMadeUnitDecks[level];
		for (var j = 0; j < deck.Count; j++) {
			var unitBuilder = deck[j];
			for (var i = 0; i < unitBuilder.count; i++) {
				var unit = unitBuilder.Setup(Faction.Enemy);
				unit.Position = _mobSpawnPosition + new Vector2(64*i, 64*j);
				GameManager.Instance.Pausable.AddChild(unit);
				EnemyUnits.Add(unit);
			}
		}
		_enemyUi.WorkingDeck.units = new Array<Unit>(EnemyUnits);
		_enemyUi.WorkingDeck.timer.Start();
	}

	public void SpawnPlayerUnits()
	{
		_testDeck.units.Clear();
		for (var i = 0; i < FullTimeUnits.Count; i++) {
			var unit = FullTimeUnits[i].Setup(Faction.Player);
			unit.Position = _playerSpawnPosition + new Vector2(64*i, 0);
			GameManager.Instance.Pausable.AddChild(unit);
			PlayerUnits.Add(unit);
		} for (var i = 0; i < OneTimeUnits.Count; i++) {
			var unit = OneTimeUnits[i].Setup(Faction.Player);
			unit.Position = _playerSpawnPosition + new Vector2(64*i, -70);
			GameManager.Instance.Pausable.AddChild(unit);
			PlayerUnits.Add(unit);
		}

		_testDeck.units = (new Array<Unit>(PlayerUnits));
	}
	
	public void AddUnitToArmy(UnitBuilder builder, bool isPermanent)
	{
		var targetList = isPermanent ? FullTimeUnits : OneTimeUnits;
		targetList.Add(builder);
		
		var icon = new TextureRect();
		icon.Texture = builder.unitBody.Weapon;
    
		icon.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
		icon.CustomMinimumSize = new Vector2(64, 64);
		icon.StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered;

		icon.SetMeta("builder", builder);
		icon.SetMeta("isPermanent", isPermanent);

		_mobsGrid.AddChild(icon);
	}

	public void RemoveUnitFromArmy()
	{
		OneTimeUnits.Clear();
		foreach (var child in _mobsGrid.GetChildren()) {
			if (child is not TextureRect icon) continue;
			var isPermanent = (bool)icon.GetMeta("isPermanent", true);
			if (!isPermanent) icon.QueueFree();
		}
	}
}
