using Godot;
using System;

public partial class PauseUi : Control
{
	[Export] Button returnButton, soundButton, exitButton;
	bool paused = false;
	bool isSoundOn = false;
	public override void _Ready()
	{
		returnButton.Pressed += ReturnToGame;
	}

	void ReturnToGame()
	{
		
	}

}
