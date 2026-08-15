using Godot;

[GlobalClass]
public partial class FishData : Resource
{
    [Export] public Texture2D Sprite;
    [Export] public int OilAmount = 1;
}
