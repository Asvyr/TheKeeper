using Godot;
using Godot.Collections;

public partial class FishSpawner : Node2D
{
	[Export] public Array<PackedScene> FishToSpawn = new Array<PackedScene>();
	[Export] private Marker2D TopLeft;
	[Export] private Marker2D BottomRight;

	[Export] public int FishCount = 10;


	public override void _Ready()
    {
        for (int i = 0; i < FishCount; i++)
        {
			SpawnFish();
        }
    }
	
	public void SpawnFish()
	{
		RandomNumberGenerator r = new RandomNumberGenerator();
		Vector2 position = new Vector2(r.RandfRange(TopLeft.Position.X, BottomRight.Position.X), r.RandfRange(TopLeft.Position.Y, BottomRight.Position.Y));

		int i = r.RandiRange(0, FishToSpawn.Count - 1);
		Fish fish = FishToSpawn[i].Instantiate<Fish>();
		fish.Position = position;

		int flip = r.RandiRange(0, 1);
		if (flip == 0) { fish.SwapDirection(); }

		AddChild(fish);

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
