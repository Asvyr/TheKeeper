using Godot;
using Godot.Collections;

public partial class SlotUi : Panel
{
	[Export] public ItemResource Item;
	[Export] public int Amount;

	[Export] TextureRect Thumbnail;
	[Export] Label AmountText;

	public void Clear()
	{
		GD.Print("Cleared Slot");
		Item = null;
		Amount = 0;
		UpdateDisplay();
	}

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

	public override void _DropData(Vector2 atPosition, Variant data)
	{
		Dictionary dic = data.AsGodotDictionary();

		// moving oil to inventory
		if (dic.ContainsKey("item"))
		{
			ItemResource item = dic["item"].As<ItemResource>();
			int amount = dic["amount"].As<int>();
			RefineryUi refinery = dic["refinery"].As<RefineryUi>();

			if (!IsInstanceValid(Item))
			{
				Item = item;
				Amount = amount;
				refinery.HeldOil = 0;
				refinery.UpdateDisplay();

				UpdateDisplay();
				return;
			}
			if (Item.ItemName == item.ItemName)
			{
				Amount = Amount + amount;
				refinery.HeldOil = 0;
				refinery.UpdateDisplay();
				UpdateDisplay();
			}
			else { return; }




		}
		// Moving items around in slots
		else if (dic.ContainsKey("slotref"))
		{
			SlotUi slot = dic["slotref"].As<SlotUi>();

			if (!IsInstanceValid(Item))
			{
				Item = slot.Item;
				Amount = slot.Amount;

				slot.Item = null;
				slot.Amount = 0;
				slot.UpdateDisplay();
				UpdateDisplay();
				return;
			}
			if (slot.Item.ItemName == Item.ItemName)
			{
				Amount = slot.Amount + Amount;

				slot.Item = null;
				slot.Amount = 0;

				slot.UpdateDisplay();
				UpdateDisplay();
				return;
			}
			else
            {
				ItemResource tmpItem = slot.Item;
				int tmpAmount = slot.Amount;

				slot.Item = Item;
				slot.Amount = Amount;

				Item = tmpItem;
				Amount = tmpAmount;

				slot.UpdateDisplay();
				UpdateDisplay();
				return;
            }
		}
	}

    public override bool _CanDropData(Vector2 atPosition, Variant data)
	{
		Dictionary dic = data.AsGodotDictionary();

		return dic.ContainsKey("item") || dic.ContainsKey("slotref");
    }



}
