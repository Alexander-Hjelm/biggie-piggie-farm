using Godot;

public partial class PlayerInventoryManager : Control
{
    private static PlayerInventoryManager _instance;

	public static PlayerInventoryManager GetInstance()
	{
		return _instance;
	}

    public override void _EnterTree()
    {
        base._EnterTree();
		if (_instance == null)
		{
			_instance = this;
		}
		else
		{
			GD.PrintErr("There are more than one PlayerInventoryManager instance in the scene. Make sure that there is only one.");
		}
    }

	public override void _Ready()
    {
        base._Ready();
        InventoryManager.GetInstance().OnSlotClick += OnSlotClickCallback;
    }

    public void ToggleInventory()
	{
		InventoryManager.GetInstance().SetInventory(Player.GetInstance().Inventory);
		InventoryManager.GetInstance().ToggleInventory();
	}

	public void OnSlotClickCallback(ItemResource item)
	{
		if (item is ToolResource)
		{
			Player.GetInstance().SetCurrentTool(item as ToolResource);
		}
	}
}