using Godot;

public partial class ButtonWithSignalNoArgs : Button
{
    [Signal]
    public delegate void OnClickEventHandler();

    [Signal]
    public delegate void OnMouseEnterEventHandler();

    [Signal]
    public delegate void OnMouseExitEventHandler();

    public override void _Ready()
    {
        base._Ready();
        Pressed += OnClickCallback;
    }

    public override void _Notification(int what)
    {
        switch (what)
        {
            case unchecked((int)NotificationMouseEnter):
                OnMouseEnterCallback();
                break;
            case unchecked((int)NotificationMouseExit):
                OnMouseExitCallback();
                break;
        }
    }

    private void OnClickCallback()
    {
        EmitSignal(SignalName.OnClick);
    }

    private void OnMouseEnterCallback()
    {
        EmitSignal(SignalName.OnMouseEnter);
    }

    private void OnMouseExitCallback()
    {
        EmitSignal(SignalName.OnMouseExit);
    }
}
