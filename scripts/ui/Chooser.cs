using Godot;
using System;
using AITUJAM2026.scripts.unit;

public partial class Chooser : Control
{
	[Export] private Button pike, bow, halberd, sword;
	public override void _Ready()
	{
		pike.Pressed += () => SelectUnit(Decks.kopie_ton);
		bow.Pressed += () => SelectUnit(Decks.bowm_an);
		halberd.Pressed += () => SelectUnit(Decks.topo_ric);
		sword.Pressed += () => SelectUnit(Decks.mech_nick);
	}

	private void SelectUnit(UnitBuilder builder)
	{
		GameManager.Instance.Arena.AddUnitToArmy(builder, true);
		QueueFree();
	}
}
