using Godot;
using System;
using AITUJAM2026.scripts.unit;

public partial class TestControll : Node
{
	private Deck deck;
	[Export] Button forward, backward, attack, heal;
	public override void _Ready()
	{
		deck = GetParent<Deck>();
		forward.Pressed += () => deck.SetAction(UnitActions.GoForward);
		backward.Pressed += () => deck.SetAction(UnitActions.GoBackward);
		heal.Pressed += () => deck.SetAction(UnitActions.Heal);
		attack.Pressed += () => deck.SetAction(UnitActions.Attack);

	}

	
}
