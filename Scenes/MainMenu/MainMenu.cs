using Godot;

public partial class MainMenu : Control
{
    [Signal] public delegate void StartGameEventHandler();


    public void _OnPlay()
    {
        EmitSignal(SignalName.StartGame);
    }

    public void _OnQuit()
    {
        GetTree().Quit();
    }
}
