using Godot;
using System;

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

	public override void _Ready()
	{
		Pressed += OnPressed;
	}

	private void OnPressed()
	{
		if (GameManager.Instance.Money <= price) return;
		switch (pressAction)
		{
			case PressAction.Tent:
				GD.Print("Tent");
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
