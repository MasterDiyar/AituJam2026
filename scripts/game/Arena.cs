using Godot;
using System;

public partial class Arena : Node2D
{
	[Export]private Button StartFight;
	
	bool isFightStarted = false;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
}
