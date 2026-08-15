using Godot;


public partial class RefineryUi : Control
{
	[Export] public ItemResource OilResource;
	[Export] public int HeldOil = 0;

	[Export] public TextureRect OilDisplay;
	[Export] public Label AmountText;

	public void _ItemsAdded(ItemResource item, int amount)
	{
		HeldOil = item.fishData.OilAmount * amount;
		UpdateDisplay();
	}

	public void UpdateDisplay()
    {
		if (HeldOil <= 0)
		{
			OilDisplay.Texture = null;
			AmountText.Text = "x0";
			AmountText.Visible = false;
		}
		else
        {
			OilDisplay.Texture = OilResource.Thumbnail;
			AmountText.Text = $"x{HeldOil}";
			if (HeldOil > 1) { AmountText.Visible = true; }
			else { AmountText.Visible = false; }
        }
    }

}
