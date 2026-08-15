using Godot;

public partial class Fish : CharacterBody2D
{
	[Export] public FishData fishData;

	[Export] public Sprite2D sprite;
	[Export] public AnimationPlayer anims;
	[Export] public RayCast2D blockCheck;

	private Vector2 Direction = Vector2.Left;


    public override void _Ready()
	{
		anims.Play("Swim");
    }


	public override void _PhysicsProcess(double delta)
	{
		if (blockCheck.IsColliding())
		{
			SwapDirection();
		}
		Velocity = Direction * fishData.SwimSpeed * (float)delta;

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
