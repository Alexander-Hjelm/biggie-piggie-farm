using Godot;

public partial class InventoryManager : Control
{
	[Export] public TabContainer TabContainer { get; set; }
	[Export] public GridContainer GridContainer { get; set; }
	[Export] public Label ItemNameLabel { get; set; }
	[Export] public RichTextLabel DescriptionLabel { get; set; }
	[Export] public ButtonWithSignalNoArgs exitButton { get; set; }
	[Export] public ItemResource[] Items = new ItemResource [32];
    [Signal] public delegate void OnSlotClickEventHandler(ItemResource item);

	private int _inventorySize = 32;

	public override void _Ready()
	{
		base._Ready();
		TabContainer.Visible = false;
		RenderItemInfo(null);
		
		// Set up inventory slot onclick callbacks
		for (int i = 0; i < _inventorySize; i++)
		{
			ItemSlot itemSlot = GridContainer.GetNode($"ItemSlot{i+1}") as ItemSlot;
			itemSlot.OnClick += OnSlotClickCallback;
			itemSlot.OnMouseEnter += OnSlotMouseEnterCallback;
			itemSlot.OnMouseExit += OnSlotMouseExitCallback;
		}

		exitButton.OnClick += OnExitButtonClickCallback;
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

	public void OnSlotClickCallback(ItemResource item)
	{
		EmitSignal(SignalName.OnSlotClick, item);
	}

	public void OnSlotMouseEnterCallback(ItemResource item)
	{
		RenderItemInfo(item);
	}

	public void OnSlotMouseExitCallback(ItemResource item)
	{
		RenderItemInfo(null);
	}

	public void OnExitButtonClickCallback()
	{
		ToggleInventory();
	}
}
