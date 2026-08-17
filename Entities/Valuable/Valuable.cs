using Godot;

public partial class Valuable : Node2D
{
	public void _Overlapped(Node2D body)
    {
		if (body.HasMethod("AddValuables"))
        {
			body.Call("AddValuables", 2);
			QueueFree();
        }
    }
}
