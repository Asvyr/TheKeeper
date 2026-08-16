using Godot;
using System;

public partial class SceneManager : Node2D
{
	[Export] MainMenu MainMenuUI;
	[Export] PackedScene PlayScene;
	[Export] PackedScene IntroScene;
	[Export] AnimationPlayer FadeAnim;

	private string CurrentScene = "mainmenu";

	public void ChangeScene(string scene)
	{
		CurrentScene = scene;

		switch (scene)
		{
			case "intro":
				FadeAnim.Play("FadeIn");
				break;
		}
	}
	
	public void UpdateScene()
	{
		MainMenuUI.Visible = false;
		Play playScene = PlayScene.Instantiate<Play>();
		AddChild(playScene);

		FadeAnim.Play("FadeOut");
	}
}
