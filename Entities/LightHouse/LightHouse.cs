using Godot;
using System;

public partial class LightHouse : Node2D
{
	[Export] public float CurrentOil = 0;
	[Export] public float RequiredOil = 20;


	public void Interact(Player player)
	{
		player.RefuelUI.UpdateDisplay(this);
		player.OpenRefuel();
	}
	

	public void _Hovered()
	{
		Player player = GetTree().Root.GetNode<Player>("Main/Play/Forground/CharacterBody2D");
		player.TargetInteraction = this;

	}

	public void _UnHovered()
	{
		Player player = GetTree().Root.GetNode<Player>("Main/Play/Forground/CharacterBody2D");
		if (player.TargetInteraction != this) { return; }
		player.TargetInteraction = null;
	}
}
