using Godot;
using System;

public partial class ResourceController : Control
{
	[Export] Label FoodCounter, MoneyCounter;

	public override void _Process(double delta)
	{
		FoodCounter.Text = $"{GameManager.Instance.Food}";
		MoneyCounter.Text = $"{GameManager.Instance.Money}";
	}
}
