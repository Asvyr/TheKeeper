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
		Player player = GetTree().Root.GetNode<Player>("Main/Play/Forground/CharacterBody2D");
		player.TargetInteraction = this;
	}
	
	public void _UnHovered()
	{
		Player player = GetTree().Root.GetNode<Player>("Main/Play/Forground/CharacterBody2D");
		if (player.TargetInteraction == this) { player.TargetInteraction = null; }

	}
}
