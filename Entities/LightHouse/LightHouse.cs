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
}
