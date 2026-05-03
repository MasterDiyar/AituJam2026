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

	[Export] private PackedScene ChooserScene;

	private void OnPressed()
	{
		if (GameManager.Instance.Money < price) return;
		GameManager.Instance.Money -= price;
		switch (pressAction)
		{
			case PressAction.Tent:var a=
				ChooserScene.Instantiate<Control>();
				a.Position = Position;
				GameManager.Instance.UI.AddChild(a);
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
