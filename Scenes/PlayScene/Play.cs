using System.Text;
using Godot;

public partial class Play : Node2D
{
	[Export] public EndOfDayMenu endOfDayMenu;
	[Export] public LightHouse lightHouse;
	[Export] public int Strikes = 0;

	public void _DayFinished()
	{
		if (lightHouse.CurrentOil >= lightHouse.RequiredOil)
		{
			endOfDayMenu.Details.Text = $"The Lighthouse remains on overnight!\nThe Boats navigate safely!";
		}
		else
		{
			Strikes += 1;
			endOfDayMenu.Details.Text = $"The lighthouse dosen't have enough fuel for the night!\nYou needed {lightHouse.RequiredOil - lightHouse.CurrentOil} more [img]res://Art Assets/Sprite-oil.png[/img]Fish Oil!";
		}

		StringBuilder strikeString = new StringBuilder();
		for (int i = 0; i < Strikes; i++)
		{
			strikeString.Append("[img=32x32]res://Art Assets/X.png[/img]");
		}
		endOfDayMenu.Strikes.Text = $"Strikes: {strikeString}";


		endOfDayMenu.Play();
	}

	public void _BodyEntered(Node2D body)
	{
		if (body.HasMethod("UpdateSwimming"))
		{
			body.Call("UpdateSwimming", false);
		}

	}

	public void _BodyExited(Node2D body)
	{
		if (body.HasMethod("UpdateSwimming"))
		{
			body.Call("UpdateSwimming", true);
		}
	}

	public override void _Ready()
	{
		endOfDayMenu = GetTree().Root.GetNode<EndOfDayMenu>("Main/CanvasLayer/EndOfDayMenu");
	}

}
