using Godot;
using System;

public partial class UpgradeBoat : Node2D
{
	public void Interact(Player player)
    {
		player.OpenUpgrades();
    }


	public void _Hovered()
	{
		GD.Print("Hovered Over Boat");
		Player player = GetTree().Root.GetNode<Player>("Main/Play/Forground/CharacterBody2D");
		player.TargetInteraction = this;
	}
	
	public void _UnHovered()
	{
		GD.Print("Unhovered boat");
		Player player = GetTree().Root.GetNode<Player>("Main/Play/Forground/CharacterBody2D");
		if (player.TargetInteraction == this) { player.TargetInteraction = null; }

	}
}
