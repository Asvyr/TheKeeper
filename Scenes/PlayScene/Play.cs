using System.Text;
using Godot;

public partial class Play : Node2D
{
	[Export] public EndOfDayMenu endOfDayMenu;
	[Export] public LightHouse lightHouse;
	[Export] public Marker2D RespawnPoint;
	[Export] public FishSpawner Spawner;
	[Export] public int Strikes = 0;
	[Export] public int Day = 1;


	public void Reset()
	{
		GetNode("ResetTimer").QueueFree();

		Player player = GetNode<Player>("Forground/CharacterBody2D");
		player.Position = RespawnPoint.Position;

		ClearFish();
		SpawnNewFish();

		endOfDayMenu.HideMenu();

		GetNode<Timer>("TimeLimit").Start();
	}

	public void ClearFish()
	{
		foreach (Node child in GetNode("FishSpawns").GetChildren())
        {
			child.QueueFree();
        }
		GD.Print("Cleared All Fish");
	}
	
	public void SpawnNewFish()
    {
		Spawner.FishCount = 300;
		Spawner.SpawnAllFish();
    }

	public void _DayFinished()
	{
		Timer resetTimer = new Timer();
		resetTimer.Name = "ResetTimer";
		resetTimer.OneShot = true;
		resetTimer.Autostart = false;
		resetTimer.WaitTime = 15;
		AddChild(resetTimer);
		resetTimer.Timeout += Reset;

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
		resetTimer.Start();
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
		Spawner.SpawnParent = GetNode<Node2D>("FishSpawns");
		endOfDayMenu = GetTree().Root.GetNode<EndOfDayMenu>("Main/CanvasLayer/EndOfDayMenu");
	}

}
