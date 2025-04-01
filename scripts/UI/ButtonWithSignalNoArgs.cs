using Godot;

public partial class ButtonWithSignalNoArgs : Button
{
    [Signal]
    public delegate void OnClickEventHandler();

    public override void _Ready()
    {
        base._Ready();
        Pressed += OnClickCallback;
    }

    private void OnClickCallback()
    {
        EmitSignal(SignalName.OnClick);
    }
}
