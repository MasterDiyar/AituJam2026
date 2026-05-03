using Godot;
using System;
using AITUJAM2026.scripts.unit;

public partial class EnemyUi : Control
{
	public string[] Names = [
	"WanderBraun", "Wilhelm II", "Otto der Große", "Leon Torres", "Adolf Fides", "Fidel Barren", "Aleksander Vanz",
	"Leon Frank", 
	], Links = ["Matveh", "Josua", "Vladislav"];

	[Export] private TextureRect Portrait;
	[Export] public Deck WorkingDeck;
	[Export] private Label NameLabel;
	readonly Vector2 startPos = new(1920, 0), endPos = new(1920, -192);
	bool whereMove = false;
	Tween moveTween;
	

	public void ToggleUi(bool toggler)
	{
		if (moveTween != null && moveTween.IsRunning())
			moveTween.Kill();

		moveTween = CreateTween();
        
		Vector2 targetPos = toggler ? startPos : endPos;
		
		moveTween.TweenProperty(this, "position", targetPos, 0.5f)
			.SetTrans(Tween.TransitionType.Back) 
			.SetEase(Tween.EaseType.Out);
	}

	public override void _Ready()
	{
		ToggleUi(false);
	}
	
	public void UpdateDisplay(string name, string link, Godot.Collections.Array<UnitActions> actions)
	{
		NameLabel.Text = name;
		Portrait.Texture = GD.Load<Texture2D>($"res://assets/texture/heads/{link}.png");
		WorkingDeck.actions = actions;
	}
}
