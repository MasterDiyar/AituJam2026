using Godot;
using System;

public partial class Game : Node2D
{
	public Node2D Pausable, NoDestroy;
	public CanvasLayer UI;
	public PauseUi pauseUi;
	public Arena Arena;
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
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("esc"))
		{
			pauseUi.ShowUI();
		}
	}
}
