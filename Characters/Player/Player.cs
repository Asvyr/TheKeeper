using Godot;
using Godot.Collections;

public partial class Player : CharacterBody2D
{
	[Export] public float WalkSpeed = 300.0f;
	[Export] public float SwimSpeed = 100.0f;
	[Export] public float JumpVelocity = -200.0f;
	[Export] public int Damage = 10;

	[Export] public bool Swimming = false;
	[Export] public float SwimGravMod = 2;
	[Export] private Sprite2D Speargun;
	[Export] private Sprite2D CharSprite;
	[Export] private Marker2D GunRotPoint;
	[Export] private float InteractionLength = 100;
	[Export] private Control RefineryUI;
	[Export] public RefuelMenu RefuelUI;
	[Export] public UpgradeMenu UpgradeUI;
	[Export] public InventoryUi inventory;

	[Export] public AnimationPlayer Anims;

	[Export] public PackedScene Harpoon;

	private bool InMenu = false;

	public Node2D TargetInteraction;

	[Export] public int Valuables = 100;

	[Export] public int MaxAmmo = 3;
	public int UsedAmmo = 0;


	public void AddValuables(int amount) { Valuables += amount; }
	

	public bool Pickup(ItemResource inItem, int amount)
	{
		Array<SlotUi> emptySlots = new Array<SlotUi>();
		for (int i = 0; i < inventory.GetChildren().Count; i++)
		{
			SlotUi slot = inventory.GetChild<SlotUi>(i);

			// add to array of empty slot indexes
			if (!IsInstanceValid(slot.Item)) { emptySlots.Add(inventory.GetChild<SlotUi>(i)); continue; }
			if (slot.Item.ItemName == inItem.ItemName)
			{
				slot.Amount = slot.Amount + amount;
				slot.UpdateDisplay();
				return true;
			}
		}

		if (emptySlots.Count <= 0) { return false; }
		emptySlots[0].Item = inItem;
		emptySlots[0].Amount = amount;

		inventory.UpdateInventory();
		return true;
	}

	public override void _Process(double delta)
	{
		GunRotPoint.LookAt(GetGlobalMousePosition());

		if (Input.IsActionJustPressed("Use")) { TryInteract(); }
		if (Input.IsActionJustPressed("Close")) { CloseAllUI(); }
		if (Input.IsActionJustPressed("Shoot"))
		{
			if (Swimming && !InMenu) { TryShoot(); }
		}

		if (Velocity.X > 0) { CharSprite.FlipH = false; }
		else if (Velocity.X < 0) { CharSprite.FlipH = true; }
	}

	public void TryShoot()
	{
		if (UsedAmmo >= MaxAmmo) { return; }

		Marker2D muzzle = GetNode<Marker2D>("RotPoint/Sprite2D/Muzzle");
		Harpoon tmpH = Harpoon.Instantiate<Harpoon>();
		tmpH.parent = this;
		tmpH.Damage = Damage;

		tmpH.GlobalPosition = muzzle.GlobalPosition;
		tmpH.GlobalRotation = muzzle.GlobalRotation;

		GetTree().Root.GetNode("Main/Play").AddChild(tmpH);
		UsedAmmo += 1;
		if (UsedAmmo >= MaxAmmo)
        {
			GetNode<Timer>("ReloadTimer").Start();
        }
	}
	
	public void _FinishedReloading() { UsedAmmo = 0; }


	public void TryInteract()
	{
		if (!IsInstanceValid(TargetInteraction)) { GD.Print("Null Interaction"); return; }
		Vector2 distance = GlobalPosition - TargetInteraction.GlobalPosition;

		if (!TargetInteraction.HasMethod("Interact")) { return; }
		if (distance.Length() > InteractionLength) { return; }

		TargetInteraction.Call("Interact", this);

	}


	//////////////////////////////////////////////////////
	/// UI RELATED
	//////////////////////////////////////////////////////

	public void OpenUpgrades()
    {
		if (InMenu) { CloseAllUI(); }
		InMenu = true;
		UpgradeUI.UpdateDisplay(this);
		UpgradeUI.Visible = true;
    }

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
		UpgradeUI.Visible = false;

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
			Anims.Play("IdleRight");

		}
		else
		{
			Speargun.Visible = false;
			Anims.Play("SwimLeft");
		}
	}



	public override void _PhysicsProcess(double delta)
	{
		if (InMenu) { return; }
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
