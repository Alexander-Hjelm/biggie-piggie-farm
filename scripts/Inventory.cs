using Godot;

public partial class Inventory : Node3D
{
	[Export] public ItemResource[] Items = new ItemResource [32];

	private int _inventorySize = 32;

	public int GetInventorySize()
    {
        return _inventorySize;
    }

    public ItemResource GetItem(int i)
    {
        return Items[i];
    }
}
