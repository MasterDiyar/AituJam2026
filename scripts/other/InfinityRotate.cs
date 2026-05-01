using Godot;
using System;

public partial class InfinityRotate : Sprite2D
{
	[Export] public float RotationSpeed = 200f;
	public override void _Process(double delta)
	{
		Rotation += (float)delta * RotationSpeed;
	}
}
