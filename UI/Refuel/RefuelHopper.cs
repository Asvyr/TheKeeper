using Godot;
using Godot.Collections;

public partial class RefuelHopper : Panel
{
	[Signal] public delegate void AddedFuelEventHandler(float amount);


	public override void _DropData(Vector2 atPosition, Variant data)
	{
		Dictionary dic = data.AsGodotDictionary();

		if (!dic.ContainsKey("item")) { return; }

		
	}

    public override bool _CanDropData(Vector2 atPosition, Variant data)
	{
		if (data.VariantType != Variant.Type.Dictionary) { return false; }

		Dictionary dic = data.AsGodotDictionary();
		return dic.ContainsKey("item");
    }


}
