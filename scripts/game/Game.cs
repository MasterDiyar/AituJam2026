using Godot;
using System;

public partial class Game : Node2D
{
	public Node2D Pausable;
	public override void _Ready()
	{
		Pausable = GetNode<Node2D>("Pausable");
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("esc"))
		{
			
		}
	}
}
