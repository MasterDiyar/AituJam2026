using Godot;
using System;

public partial class UnitBody : Node2D
{
	[Export] public Sprite2D Body, Cloth, Head, Hat, Weapon, Secondary;
	[Export] public UnitResource uResource;
	public override void _Ready()
	{
		uResource.SetUnitBody(this);
	}
}
