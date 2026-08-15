using Godot;

[GlobalClass]
public partial class ItemResource : Resource
{
    [Export] public string ItemName;
    [Export] public Texture2D Thumbnail;
    [Export] public FishData fishData;
}
