using Godot;

public partial class StatusBar : Control
{
    [Export]
    public ItemSlot playerCurrentToolItemSlot;

    public override void _Ready()
    {
        base._Ready();
        Player.GetInstance().CurrentToolChanged += CurrentToolChangedCallback;
    }

    private void CurrentToolChangedCallback(ToolResource tool)
    {
        playerCurrentToolItemSlot.TextureRect.Texture = tool.Icon;
    }
}
