using Godot;
using System;

public partial class Menu : Control
{
	[Export] public Button PlayButton;
	public override void _Ready()
	{
		PlayButton.Pressed += OnPlay;
	}

	void OnPlay()
	{
		var scene =  GD.Load<PackedScene>("res://scenes/management/game.tscn").Instantiate<Game>();
		var arena = GD.Load<PackedScene>("res://scenes/maps/arena.tscn").Instantiate();
		
		GetParent().AddChild(scene);
		scene.Pausable.AddChild(arena); 
		QueueFree();
	}
}
