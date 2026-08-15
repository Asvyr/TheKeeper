using Godot;

public partial class Play : Node2D
{

    public void _BodyEntered(Node2D body)
    {
        if (body.HasMethod("UpdateSwimming"))
        {
            body.Call("UpdateSwimming", false);
        }

    }
    
    public void _BodyExited(Node2D body)
    {
        if (body.HasMethod("UpdateSwimming"))
        {
            body.Call("UpdateSwimming", true);
        }
    }
}
