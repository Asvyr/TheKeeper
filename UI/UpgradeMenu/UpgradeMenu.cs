using Godot;

public partial class UpgradeMenu : Control
{
	[Export] private RichTextLabel Valuables;
	[Export] private RichTextLabel DamageLabel;
	[Export] private RichTextLabel SwimLabel;
	[Export] private RichTextLabel AmmoLabel;

	[Export] private AudioStreamPlayer2D Valid;
	[Export] private AudioStreamPlayer2D Invalid;

	[Export] private string IconString = "[img]res://Art Assets/DevValuable.png[/img]";

	[Export] private int DamageCost = 5;
	[Export] private int SpeedCost = 5;
	[Export] private int AmmoCost = 5;

	Player player;


    public override void _Ready()
	{
		DamageLabel.Text = IconString + DamageCost;
		SwimLabel.Text = IconString + SpeedCost;
		AmmoLabel.Text = IconString + AmmoCost;

    }




	public void UpdateDisplay(Player inPlayer)
	{
		player = inPlayer;
		Valuables.Text = $"[img]res://Art Assets/DevValuable.png[/img]: {player.Valuables}";
	}

	public void BuyDamage()
	{
		if (player.Valuables >= DamageCost)
		{
			player.Valuables -= DamageCost;
			player.Damage += 10;
			UpdateDisplay(player);
			Valid.Play();
			return;
		}

		Invalid.Play();
	}

	public void BuySpeed()
	{
		if (player.Valuables >= SpeedCost)
		{
			player.Valuables -= SpeedCost;
			player.SwimSpeed += 350;
			UpdateDisplay(player);
			Valid.Play();
			return;
		}

		Invalid.Play();
	}
	
	public void BuyAmmo()
    {
		if (player.Valuables >= AmmoCost)
		{
			player.Valuables -= AmmoCost;
			player.MaxAmmo += 1;
			UpdateDisplay(player);
			Valid.Play();
			return;
		}

		Invalid.Play();
    }
}
