using System.Linq;
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

    public void AddItem(ItemResource itemResource)
	{
		for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
            {
                Items[i] = itemResource;
                break;
            }
        }
	}
}
