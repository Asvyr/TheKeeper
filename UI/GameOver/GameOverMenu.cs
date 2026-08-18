using Godot;
using System;

public partial class GameOverMenu : Control
{
    public void GotoMainMenu()
    {
		GetTree().ChangeSceneToFile("res://Scenes/Main.tscn");
    }
}
