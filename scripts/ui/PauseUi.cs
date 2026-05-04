using Godot;
using System;

public partial class PauseUi : Control
{
	[Export] Button returnButton, soundButton, exitButton;
	public bool paused = false;
	bool isSoundOn = false;
	public override void _Ready()
	{
		returnButton.Pressed += ReturnToGame;
		soundButton.Pressed += ToggleSound;
		Hide();
	}
	
	void ToggleSound()
	{
		isSoundOn = !isSoundOn;
		GameManager.Instance.Game.Audio.VolumeDb = isSoundOn ? -110 : -25;
	}

	void ReturnToGame()
	{
		paused = false;
		Hide();
		GameManager.Instance.Pausable.ProcessMode = ProcessModeEnum.Inherit;
	}
	
	public void ShowUI()
	{
		paused = true;
		Show();
		GameManager.Instance.Pausable.ProcessMode = ProcessModeEnum.Disabled;
	}

}
