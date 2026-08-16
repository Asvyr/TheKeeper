using Godot;

[GlobalClass]
public partial class FishData : Resource
{
    [Export] public float SwimSpeed = 100;
    [Export] public int MaxHealth = 10;
    [Export] public Texture2D Sprite;
    [Export] public int OilAmount = 1;
}
