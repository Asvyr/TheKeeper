using Godot;

public partial class Reload : ProgressBar
{
	[Export] Player player;
	Timer ReloadTimer;

    public override void _Ready()
	{
		ReloadTimer = player.GetNode<Timer>("ReloadTimer");
    }


    public override void _Process(double delta)
	{
		if (player.UsedAmmo >= player.MaxAmmo) { Visible = true; }
		else { Visible = false; }

		Value = 1 - (ReloadTimer.TimeLeft / ReloadTimer.WaitTime);
    }

	
}
