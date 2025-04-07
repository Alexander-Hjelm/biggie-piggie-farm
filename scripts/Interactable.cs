using Godot;

public partial class Interactable : Node3D
{
    [Signal] public delegate void InteractedEventHandler();

    private bool _isInteractable = false;

    public void Interact()
    {
        EmitSignal(SignalName.Interacted);
    }

    public void SetIsInteractable(bool isInteractable)
    {
        _isInteractable = isInteractable;
    }

    public bool IsInteractable()
    {
        return _isInteractable;
    }
}
