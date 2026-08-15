using Godot;
using Godot.Collections;

public partial class OilHold : TextureRect
{
	[Export] public RefineryUi refinery;


    public override Variant _GetDragData(Vector2 atPosition)
	{
		if (refinery.HeldOil <= 0) { return atPosition; }

		TextureRect preview = new() { Texture = Texture, ExpandMode = ExpandModeEnum.IgnoreSize };
		preview.Size = Vector2.One * 64;
		SetDragPreview(preview);

		Dictionary dic = new Dictionary
		{
			{"item", refinery.OilResource},
			{"amount", refinery.HeldOil },
			{"refinery", refinery }
		};

		return dic;
    }



	public override bool _CanDropData(Vector2 atPosition, Variant data)
	{
		return false;
	}



}
