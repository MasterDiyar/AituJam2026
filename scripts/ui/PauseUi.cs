using Godot;
using System;

public partial class PauseUi : Control
{
	[Export] Button returnButton, soundButton, exitButton;
	bool paused = false;
	bool isSoundOn = false;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
}
