using Godot;

public partial class Harpoon : CharacterBody2D
{
	[Export] public int Damage = 10;
	[Export] public float Speed = 500;
	public Node2D parent;


	public void _OnOverlap(Node2D body)
	{
		GD.Print(body.Name);

		if (body == parent) { return; }
		if (body.HasMethod("TakeDamage"))
		{
			body.CallDeferred("TakeDamage", Damage);
		}

		QueueFree();
	}

    public override void _PhysicsProcess(double delta)
	{
		Velocity = new Vector2(0, -Speed).Rotated(Rotation);
		MoveAndSlide();
    }



	public void _LifeOver() { QueueFree(); }
}
