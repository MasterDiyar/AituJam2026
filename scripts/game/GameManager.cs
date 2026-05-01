using Godot;
using System;
using System.Linq;

public partial class GameManager : Node
{
	public static GameManager Instance;
	public Node2D Pausable, NoDestroy;
	public PauseUi PauseUI;
	public Game Game;
	public CanvasLayer UI;
	
	bool isPaused = false;

	public float Money = 0, Food = 0;
	
	Action OnMoneyChange, OnFoodChange;
	
	public override void _Ready()
	{
		if (Instance == null)
			Instance = this;
		else QueueFree();
	}
	
	public void AddToNoDestroy(params object[] items)
	{
		foreach (var node in items.OfType<Node>())
			node.Reparent(NoDestroy);
	}

	public void ReturnToPausable(Node ssilka)
	{
		foreach (var node in NoDestroy.GetChildren())
			node.Reparent(ssilka);
	}

	public void TogglePause(bool pause = true){
		isPaused = pause;
		Pausable.ProcessMode = ProcessModeEnum.Pausable;
	}
}
