using Godot;
using System;
using AITUJAM2026.scripts.unit;

public partial class Game : Node2D
{
	public Node2D Pausable, NoDestroy;
	public CanvasLayer UI;
	public PauseUi pauseUi;
	public Arena Arena;
	
	public int enemyUnitCount = 0, playerUnitCount = 0;
	public override void _Ready()
	{
		Pausable = GetNode<Node2D>("Pausable");
		UI = GetNode<CanvasLayer>("UI");
		NoDestroy = GetNode<Node2D>("NoDestroy");
		pauseUi = GetNode<PauseUi>("UI/PauseUI");
		GameManager.Instance.Pausable =  Pausable;
		GameManager.Instance.NoDestroy =  NoDestroy;
		GameManager.Instance.UI = UI;
		GameManager.Instance.PauseUI = pauseUi;
		
	}

	public void AfterInit()
	{
		Arena.StartFight = UI.GetNode<Button>("StartFightButton");
		GameManager.Instance.Arena = Arena;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("esc"))
		{
			pauseUi.ShowUI();
		}
	}

	public void StartFight()
	{
		foreach (var ch in Pausable.GetChildren())
		{
			if (ch is not Unit uit) continue;
			if (uit.UnitFaction == Faction.Enemy) enemyUnitCount++;
			if (uit.UnitFaction == Faction.Player) playerUnitCount++;
		}
	}

	public void OneDie()
	{
		if (enemyUnitCount == 0)
		{
			Arena.OnWin();
		}
		else if (playerUnitCount == 0)
		{
			Arena.OnLoose();
		}
	}
}
