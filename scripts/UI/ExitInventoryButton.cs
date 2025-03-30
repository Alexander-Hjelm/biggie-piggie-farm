using Godot;

public partial class ExitInventoryButton : Button
{
    public override void _Ready()
    {
        base._Ready();
        Pressed += OnClickCallback;
    }

    private void OnClickCallback()
    {
        InventoryManager._instance.ToggleInventory();
    }
}
