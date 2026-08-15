using Godot;
using Godot.Collections;

public partial class SlotUi : Panel
{
	[Export] public ItemResource Item;
	[Export] public int Amount;

	[Export] TextureRect Thumbnail;
	[Export] Label AmountText;


	public void UpdateDisplay()
	{
		if (!IsInstanceValid(Item))
		{
			Thumbnail.Texture = null;
			AmountText.Text = "x0";
			AmountText.Visible = false;
		}
		else
		{
			Thumbnail.Texture = Item.Thumbnail;
			AmountText.Text = $"x{Amount}";
			AmountText.Visible = true;
		}

	}


    public override Variant _GetDragData(Vector2 atPosition)
	{
		if (!IsInstanceValid(Item)) { return new Dictionary(); }

		TextureRect preview = new() { Texture = Thumbnail.Texture, ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize };
		preview.CustomMinimumSize = Vector2.One * 64;
		SetDragPreview(preview);

		Dictionary dic = new Dictionary
		{
			{"slotref", this}
		};

		return dic;
    }

}
