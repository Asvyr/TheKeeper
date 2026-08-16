using Godot;

public partial class Fish : CharacterBody2D
{
	[Export] public ItemResource Data;
	[Export] public Sprite2D sprite;
	[Export] public AnimationPlayer anims;
	[Export] public RayCast2D blockCheck;
	[Export] public PackedScene DroppedFish;

	private Vector2 Direction = Vector2.Left;
	private int CurrentHealth = 1;
	private float FleeSpeedMod = 2000;
	private float SpeedMod = 0;
	private float FleeTime = 1.5f;


	public override void _Ready()
	{
		CurrentHealth = Data.fishData.MaxHealth;
		sprite.Texture = Data.fishData.Sprite;
	}

	public void TakeDamage(int amount)
	{
		CurrentHealth = CurrentHealth - amount;
		GetNode<AnimationPlayer>("AnimationPlayer").Play("Hit");
		if (CurrentHealth <= 0) { CallDeferred("Die"); return; }

		SpeedMod = FleeSpeedMod;
		GetNode<Timer>("FleeTimer").Start();
	}

	public void _ResetFlee()
    {
		SpeedMod = 0;
    }

	public void Die()
	{
		FishPickup drop = DroppedFish.Instantiate<FishPickup>();
		drop.GlobalPosition = GlobalPosition;
		drop.FishItem = Data;
		GetTree().Root.GetNode("Main/Play/FishSpawns").AddChild(drop);
		Visible = false;
		QueueFree();
	}


	public override void _PhysicsProcess(double delta)
	{
		if (blockCheck.IsColliding())
		{
			SwapDirection();
		}
		Velocity = Direction * (Data.fishData.SwimSpeed + SpeedMod) * (float)delta;

		MoveAndSlide();
	}
	
	public void SwapDirection()
	{
		if (Direction == Vector2.Left)
		{
			Direction = Vector2.Right; sprite.FlipH = true;
			blockCheck.Rotation = 180;
		}
		else
		{
			Direction = Vector2.Left; sprite.FlipH = false;
			blockCheck.Rotation = 0;
		}
	}

}
