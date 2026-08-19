using Godot;
using System;

public partial class RefuelMenu : Control
{
	[Export] ProgressBar SupplyBar;

	[Export] public AudioStreamPlayer2D AddedSound;

	public LightHouse lightHouse;


	public void UpdateDisplay(LightHouse inHouse)
	{
		lightHouse = inHouse;
		SupplyBar.MaxValue = inHouse.RequiredOil;
		SupplyBar.Value = inHouse.CurrentOil;
	}

	public void _OnAddedFuel(float amount)
	{
		lightHouse.CurrentOil = lightHouse.CurrentOil + amount;
		AddedSound.Play();
		UpdateDisplay(lightHouse);
	}


	

}
