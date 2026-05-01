using Godot;
using System;
using AITUJAM2026.scripts.unit;

public partial class ActionContainer : HBoxContainer
{
	private Deck deck;
	[Export]public Godot.Collections.Dictionary<UnitActions, Texture2D> Textures;
	public override void _Ready()
	{
		deck = GetParent<Deck>();
		SetDecK();
		deck.ActionChanged += OnActionChanged;
	}

	private TextureRect curr;

	void OnActionChanged(int num)
	{
		curr?.Hide();
		var a =GetChild<Control>(num);
		curr = a.GetChild<TextureRect>(0);
		curr.Show();
	}

	
	void SetDecK()
	{
		
		foreach (var act in deck.actions)
		{
			Control ctrl = new();
			TextureRect a = new(), glow = new();
			ctrl.CustomMinimumSize = new Vector2(64, 64);
			a.Texture = Textures[act];
			a.Position = Vector2.One;
			
			glow.Texture = Textures[act];
			glow.CustomMinimumSize = new Vector2(66, 66);
			glow.Modulate = new (2, 2, 2, 2);
			glow.Hide();
			ctrl.AddChild(glow);
			ctrl.AddChild(a);
			AddChild(ctrl);
		}
	}
}
