using Godot;

public partial class UpgradeMenu : Control
{
	[Export] private RichTextLabel Valuables;

	Player player;



	public void UpdateDisplay(Player inPlayer)
	{
		player = inPlayer;
		Valuables.Text = $"[img]res://Art Assets/DevValuable.png[/img]: {player.Valuables}";
    }
}
