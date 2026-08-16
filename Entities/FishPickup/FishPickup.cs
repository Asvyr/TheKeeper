using Godot;
using System;

public partial class FishPickup : Node2D
{
	[Export] public ItemResource FishItem;
	[Export] public Sprite2D sprite;
	[Export] public GpuParticles2D Particle;

	public override void _Ready()
	{
		sprite.Texture = FishItem.Thumbnail;
		Particle.Emitting = true;
		RandomNumberGenerator r = new RandomNumberGenerator();

		float pitch = r.RandfRange(0.85f, 1.15f);
		AudioStreamPlayer2D bubble = GetNode<AudioStreamPlayer2D>("BubbleAudio");

		bubble.PitchScale = pitch;
		bubble.Play();
	}

	public void _BodyEntered(Node2D body)
    {
		if (!body.HasMethod("Pickup")) { return; }

		bool success = (bool)body.Call("Pickup", FishItem, 1);
		if (success) {QueueFree(); }
    }
	
}
