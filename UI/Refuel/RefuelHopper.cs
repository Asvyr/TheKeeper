using Godot;
using Godot.Collections;

public partial class RefuelHopper : Panel
{
	[Signal] public delegate void AddedFuelEventHandler(float amount);


	public override void _DropData(Vector2 atPosition, Variant data)
	{
		Dictionary dic = data.AsGodotDictionary();

		if (!dic.ContainsKey("slotref")) { return; }
		SlotUi slot = dic["slotref"].As<SlotUi>();

		if (!IsInstanceValid(slot.Item)) { return; }
		if (slot.Item.ItemName == "Fish Oil")
		{
			EmitSignal(SignalName.AddedFuel, slot.Amount);
			slot.Item = null;
			slot.Amount = 0;
			slot.UpdateDisplay();
		}
		

		
	}

    public override bool _CanDropData(Vector2 atPosition, Variant data)
	{
		if (data.VariantType != Variant.Type.Dictionary) { return false; }

		Dictionary dic = data.AsGodotDictionary();
		return dic.ContainsKey("slotref");
	}

	
}
