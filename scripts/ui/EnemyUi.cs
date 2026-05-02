using Godot;
using System;
using AITUJAM2026.scripts.unit;

public partial class EnemyUi : Control
{
	private string[] Names = [
	"WanderBraun", "Otto von Altmark", "Otton", "Leon Torres", "Adolf Fides", "Fidel Barren", "Aleksander Vanz",
	"Leon Frank", 
	], Links = ["Matveh", "Josua", "Vladislav"];

	[Export] private TextureRect Portrait;
	
	[Export] private Label NameLabel;
	readonly Vector2 startPos = new(1152, 0), endPos = new(1152, -192);
	bool whereMove = false;
	Tween moveTween;
	[Export] private Deck WorkingDeck;

	public void ToggleUi(bool toggler)
	{
		if (moveTween != null && moveTween.IsRunning())
			moveTween.Kill();

		moveTween = CreateTween();
        
		Vector2 targetPos = toggler ? startPos : endPos;

		if (toggler)
			SetRandomPerson();
		
		moveTween.TweenProperty(this, "position", targetPos, 0.5f)
			.SetTrans(Tween.TransitionType.Back) 
			.SetEase(Tween.EaseType.Out);
	}


	private void SetRandomPerson()
	{
		NameLabel.Text = Names[new Random().Next(0, Names.Length)];
		Portrait.Texture = GD.Load<Texture2D>($"res://assets/texture/{Links[new Random().Next(0, Links.Length)]}.png");
	}
	
}
