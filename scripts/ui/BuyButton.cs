using Godot;
using System;
using AITUJAM2026.scripts.unit;

public partial class BuyButton : Button
{
	public enum PressAction
	{
		Tent,
		Farm,
		Tavern,
		Forge
	}

	[Export] private PressAction pressAction;
	[Export] private float price;
	[Export] private CanvasItem hidingSprite;

	private UnitBuilder[] link = [Decks.kopie_ton, Decks.bowm_an, Decks.topo_ric, Decks.mech_nick];

	public override void _Ready()
	{
		Pressed += OnPressed;
	}

	private void OnPressed()
	{
		if (GameManager.Instance.Money < price) return;
		switch (pressAction)
		{
			case PressAction.Tent:
				GameManager.Instance.Arena.AddUnitToArmy(link[GD.RandRange(0, link.Length)], true);
				break;
			case PressAction.Farm:
				GameManager.Instance.AddictiveHp += 4;
				break;
			case PressAction.Forge:
				GameManager.Instance.AddictiveDamage += 2;
				break;
		}
		hidingSprite.Show();
		QueueFree();
	}
}
