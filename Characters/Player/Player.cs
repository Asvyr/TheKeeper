using Godot;

public partial class Player : CharacterBody2D
{
	[Export] public float WalkSpeed = 300.0f;
	[Export] public float SwimSpeed = 100.0f;
	[Export] public float JumpVelocity = -200.0f;

	[Export] public bool Swimming = false;
	[Export] public float SwimGravMod = 2;
	[Export] private Sprite2D Speargun;
	[Export] private Marker2D GunRotPoint;
	[Export] private float InteractionLength = 200;
	[Export] private Control RefineryUI;
	[Export] public RefuelMenu RefuelUI;

	private bool InMenu = false;

	public Node2D TargetInteraction;


	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
		GunRotPoint.LookAt(GetGlobalMousePosition());

		if (Input.IsActionJustPressed("Use")) { TryInteract(); }
		if (Input.IsActionJustPressed("Close")) { CloseAllUI(); }
	}


	public void TryInteract()
	{
		if (!IsInstanceValid(TargetInteraction)) { return; }
		Vector2 distance = GlobalPosition - TargetInteraction.GlobalPosition;

		if (!TargetInteraction.HasMethod("Interact")) { return; }
		if (distance.Length() > InteractionLength) { return; }

		TargetInteraction.Call("Interact", this);

	}


	//////////////////////////////////////////////////////
	/// UI RELATED
	//////////////////////////////////////////////////////

	public void OpenRefinery()
	{
		if (InMenu) { CloseAllUI(); }
		InMenu = true;
		RefineryUI.Visible = true;
	}

	public void OpenRefuel()
    {
		if (InMenu) { CloseAllUI(); }
		InMenu = true;
		RefuelUI.Visible = true;
    }


	public void CloseAllUI()
    {
		RefineryUI.Visible = false;
		RefuelUI.Visible = false;

		InMenu = false;
    }




	//////////////////////////////////////////////////////
	/// MOVEMENT RELATED
	//////////////////////////////////////////////////////

	public void UpdateSwimming(bool isSwimming)
	{
		Swimming = isSwimming;
		if (isSwimming)
		{
			Speargun.Visible = true;

		}
		else { Speargun.Visible = false; }
	}



	public override void _PhysicsProcess(double delta)
	{
		if (!Swimming) { Walk(delta); }
		else { Swim(delta); }
		MoveAndSlide();
	}


	public void Swim(double delta)
	{
		// Swimming gravity
		Vector2 velocity = Vector2.Zero;
		velocity += Vector2.Down * (GetGravity() / SwimGravMod) * (float)delta;

		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		velocity += direction * SwimSpeed;

		Velocity = velocity;
    }

	public void Walk(double delta)
    {
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * WalkSpeed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, WalkSpeed);
		}

		Velocity = velocity;
	}
}
