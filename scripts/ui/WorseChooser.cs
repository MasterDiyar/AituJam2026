using Godot;
using System;
using AITUJAM2026.scripts.unit;

public partial class WorseChooser : Control
{
	private float[] price = [2, 3, 5];
	[Export] private Button pitch, kopie, zei;
	public override void _Ready()
	{
		pitch.Pressed += () => SelectUnit(Decks.farmberg, 0);
		kopie.Pressed += () => SelectUnit(Decks.kopie_ton, 1);
		zei.Pressed += () => SelectUnit(Decks.zweih_an, 2);
	}
	
	private void SelectUnit(UnitBuilder builder, int i)
	{
		if (GameManager.Instance.Food < price[i]) return;
		GameManager.Instance.Food  -= price[i];
		GameManager.Instance.Arena.AddUnitToArmy(builder, false);
	}

	
}
