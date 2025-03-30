using Godot;

public partial class ItemSlot : Control
{
    [Export]
    public TextureRect TextureRect;

    private ItemResource _item;

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
        InventoryManager._instance.RenderItemInfo(_item);
    }

    private void OnMouseExitCallback()
    {
        InventoryManager._instance.RenderItemInfo(null);
    }
}
