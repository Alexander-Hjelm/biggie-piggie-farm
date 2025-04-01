using Godot;

public partial class ItemSlot : Control
{
    [Export]
    public TextureRect TextureRect;

    [Export]
    public ButtonWithSignalNoArgs Button;

    [Signal]
    public delegate void OnClickEventHandler(ItemResource item);

    [Signal]
    public delegate void OnMouseEnterEventHandler(ItemResource item);

    [Signal]
    public delegate void OnMouseExitEventHandler(ItemResource item);

    private ItemResource _item;

    public override void _Ready()
    {
        base._Ready();
        Button.OnClick += OnClickCallback;
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

    public void SetItem(ItemResource item)
    {
        if(item != null)
        {
            _item = item;
            TextureRect.Texture = item.Icon;
            TextureRect.Visible = true;
        }
        else
        {
            _item = null;
            TextureRect.Visible = false;
        }
    }

    public ItemResource GetItem()
    {
        return _item;
    }

    private void OnMouseEnterCallback()
    {
        EmitSignal(SignalName.OnMouseEnter, _item);
    }

    private void OnMouseExitCallback()
    {
        EmitSignal(SignalName.OnMouseExit, _item);
    }

    private void OnClickCallback()
    {
        EmitSignal(SignalName.OnClick, _item);
    }
}
