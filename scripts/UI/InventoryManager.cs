using System.Collections.Generic;
using Godot;

public partial class InventoryManager : Control
{
	public static InventoryManager _instance { get; private set; }

	[Export]
	public TabContainer TabContainer { get; set; }

	[Export]
	public GridContainer GridContainer { get; set; }

	[Export]
	public Label ItemNameLabel { get; set; }

	[Export]
	public RichTextLabel DescriptionLabel { get; set; }

	[Export]
	public ItemResource[] Items = new ItemResource [32];

	private int _inventorySize = 32;

    public override void _EnterTree()
    {
        base._EnterTree();
		if (_instance == null)
		{
			_instance = this;
		}
		else
		{
			GD.PrintErr("There are more than one InventoryManager instance in the scene. Make sure that there is only one.");
		}
    }

	public override void _Ready()
	{
		base._Ready();
		TabContainer.Visible = false;
		RenderItemInfo(null);
	}

	public void ToggleInventory()
	{
		TabContainer.Visible = !TabContainer.Visible;
		if (TabContainer.Visible)
		{
			RenderInventory();
		}
	}

	public void RenderInventory()
	{
		for (int i = 0; i < _inventorySize; i++)
		{
			ItemSlot itemSlot = GridContainer.GetNode($"ItemSlot{i+1}") as ItemSlot;
			itemSlot.SetItem(Items[i]);
		}
	}

	public void RenderItemInfo(ItemResource item)
	{
		if (item != null)
		{
			ItemNameLabel.Text = item.Name;
			DescriptionLabel.Text = item.Description;
		}
		else
		{
			ItemNameLabel.Text = "";
			DescriptionLabel.Text = "";
		}
	}
}
