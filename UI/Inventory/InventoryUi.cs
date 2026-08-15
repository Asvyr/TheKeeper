using Godot;

public partial class InventoryUi : Control
{
	public void UpdateInventory()
	{
		foreach (Node child in GetChildren())
		{
			child.Call("UpdateDisplay");
		}
	}

    public override void _Ready()
	{
		UpdateInventory();
    }

}
