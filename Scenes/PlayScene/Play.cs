using System.Text;
using Godot;

public partial class Play : Node2D
{
	[Export] public UpgradeBoat upgradeBoat;
	[Export] public EndOfDayMenu endOfDayMenu;
	[Export] public LightHouse lightHouse;
	[Export] public Marker2D RespawnPoint;
	[Export] public FishSpawner Fishspawner;
	[Export] public ValuableSpawn Valuablespawner;
	[Export] public AnimationPlayer Anims;
	[Export] public int Strikes = 0;
	[Export] public int Day = 1;

	private int DaysSinceBoat = 0;



	public void Reset()
	{
		GetNode("ResetTimer").QueueFree();

		Player player = GetNode<Player>("Forground/CharacterBody2D");
		player.Position = RespawnPoint.Position;

		lightHouse.CurrentOil = 0;

		ClearFish();
		SpawnNewFish();
		SpawnNewValuables();


		Day += 1;
		DaysSinceBoat += 1;
		if (DaysSinceBoat >= 2)
		{
			upgradeBoat.Visible = true;
			DaysSinceBoat = 0;
		}
		else { upgradeBoat.Visible = false; }


		endOfDayMenu.HideMenu();

		GetNode<Timer>("TimeLimit").Start();
		Anims.Play("DayToNight");

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
		Fishspawner.FishCount = 300;
		Fishspawner.SpawnAllFish();
	}
	
	public void SpawnNewValuables()
	{
		Valuablespawner.NumValuables = 30;
		Valuablespawner.SpawnAllValuables();
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

		if (Strikes >= 3)
        {
			GetTree().Root.GetNode<AnimationPlayer>("Main/CanvasLayer/GameOverMenu/AnimationPlayer").Play("FadeIn");
			return;
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
		endOfDayMenu = GetTree().Root.GetNode<EndOfDayMenu>("Main/CanvasLayer/EndOfDayMenu");

		Timer timer = GetNode<Timer>("TimeLimit");

		Anims.SpeedScale = (float)(1 / timer.WaitTime);

		Anims.Play("DayToNight");
	}

}
