using Godot;
using Godot.Collections;

public partial class RefineryHopper : Panel
{
	[Signal] public delegate void ItemsInputEventHandler(ItemResource item, int amount);



    public override void _DropData(Vector2 atPosition, Variant data)
	{
		Dictionary dic = data.AsGodotDictionary();

		SlotUi slot = dic["slotref"].As<SlotUi>();
		if (!IsInstanceValid(slot.Item)) { return; }
		if (!IsInstanceValid(slot.Item.fishData)) { return; }

		EmitSignal(SignalName.ItemsInput, slot.Item, slot.Amount);
		slot.Item = null;
		slot.Amount = 0;
		slot.UpdateDisplay();
    }


	public override bool _CanDropData(Vector2 atPosition, Variant data)
	{
		if (data.VariantType != Variant.Type.Dictionary) { return false; }
		Dictionary dic = data.AsGodotDictionary();

		if (!dic.ContainsKey("slotref")) { return false; }
		return true;
	}
}
