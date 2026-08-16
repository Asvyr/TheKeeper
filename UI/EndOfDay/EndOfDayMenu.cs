using Godot;
using Godot.Collections;

public partial class EndOfDayMenu : Control
{
	[Export] public RichTextLabel Details;
	[Export] public RichTextLabel Strikes;
	[Export] AnimationPlayer anims;

	public void Play()
	{
		Visible = true;
		anims.Play("FadeIn");
	}
	
	public void HideMenu()
    {
		anims.Play("FadeOut");
    }

}
