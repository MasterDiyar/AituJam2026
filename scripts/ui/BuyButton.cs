using Godot;
using System;

public partial class BuyButton : Button
{
	public enum PressAction
	{
		Tent,
		Farm,
		Bread,
		Forge
	}

	[Export] private PressAction pressAction;
	[Export] private float price;
	[Export] private Sprite2D hidingSprite;

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
				GD.Print("Farm");
				break;
			case PressAction.Bread:
				GD.Print("Bread");
				break;
			case PressAction.Forge:
				GD.Print("Forge");
				break;
		}
		hidingSprite.Show();
		QueueFree();
	}
}
