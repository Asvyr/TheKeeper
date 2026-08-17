using Godot;

public partial class ValuableSpawn : Node2D
{
	[Export] private Marker2D TopLeft;
	[Export] private Marker2D BottomRight;
	[Export] public Node2D SpawnParent;
	[Export] public PackedScene valuable;

	[Export] public int NumValuables = 10;


	public override void _Ready()
	{
		SpawnAllValuables();
	}
	
	public void SpawnAllValuables()
    {
		for (int i = 0; i < NumValuables; i++)
		{
			SpawnValuable();
		}
	}

	public void SpawnValuable()
	{
		RandomNumberGenerator r = new RandomNumberGenerator();
		Vector2 position = new Vector2(r.RandfRange(TopLeft.GlobalPosition.X, BottomRight.GlobalPosition.X), r.RandfRange(TopLeft.GlobalPosition.Y, BottomRight.GlobalPosition.Y));

		Valuable newValuable = valuable.Instantiate<Valuable>();
		newValuable.GlobalPosition = position;
		SpawnParent.AddChild(newValuable);

    }
}
