using Godot;
using System;

public partial class RefuelMenu : Control
{
	[Export] ProgressBar SupplyBar;

	private LightHouse lightHouse;


	public void UpdateDisplay(LightHouse inHouse)
	{
		lightHouse = inHouse;
		SupplyBar.MaxValue = inHouse.RequiredOil;
		SupplyBar.Value = inHouse.CurrentOil;
	}
	
	public void _OnAddedFuel(float amount)
	{
		lightHouse.CurrentOil = lightHouse.CurrentOil + amount;
		UpdateDisplay(lightHouse);
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
