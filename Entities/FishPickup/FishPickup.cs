using Godot;
using System;

public partial class FishPickup : Node2D
{
	[Export] public ItemResource FishItem;
	[Export] public Sprite2D sprite;

	public override void _Ready()
	{
		sprite.Texture = FishItem.Thumbnail;
	}

	public void _BodyEntered(Node2D body)
    {
		if (!body.HasMethod("Pickup")) { return; }

		bool success = (bool)body.Call("Pickup", FishItem, 1);
		if (success) {QueueFree(); }
    }
	
}
