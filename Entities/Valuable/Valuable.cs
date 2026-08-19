using Godot;
using Godot.Collections;

public partial class Valuable : Node2D
{
    [Export] Array<Texture2D> textures = new Array<Texture2D>();
    [Export] Sprite2D sprite;

    public override void _Ready()
    {
        RandomNumberGenerator r = new RandomNumberGenerator();
        int index = r.RandiRange(0, textures.Count - 1);

        sprite.Texture = textures[index];
    }


    public void _Overlapped(Node2D body)
    {
        if (body.HasMethod("AddValuables"))
        {
            body.Call("AddValuables", 2);
            QueueFree();
        }
    }
}
