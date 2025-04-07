using Godot;

public partial class InventoryManager : Control
{
	[Export] public TabContainer TabContainer { get; set; }
	[Export] public GridContainer GridContainer { get; set; }
	[Export] public Label ItemNameLabel { get; set; }
	[Export] public RichTextLabel DescriptionLabel { get; set; }
	[Export] public ButtonWithSignalNoArgs exitButton { get; set; }
	private Inventory _inventory;
    [Signal] public delegate void OnSlotClickEventHandler(ItemResource item);

	private static InventoryManager _instance;

	public static InventoryManager GetInstance()
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
			GD.PrintErr("There are more than one InventoryManager instance in the scene. Make sure that there is only one.");
		}
    }

	public override void _Ready()
	{
		base._Ready();
		TabContainer.Visible = false;
		RenderItemInfo(null);

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

	public void SetInventory(Inventory inventory)
	{
		_inventory = inventory;

		// Set up inventory slot onclick callbacks
		for (int i = 0; i < _inventory.GetInventorySize(); i++)
		{
			ItemSlot itemSlot = GridContainer.GetNode($"ItemSlot{i+1}") as ItemSlot;
			itemSlot.OnClick += OnSlotClickCallback;
			itemSlot.OnMouseEnter += OnSlotMouseEnterCallback;
			itemSlot.OnMouseExit += OnSlotMouseExitCallback;
		}
	}

	public void RenderInventory()
	{
		for (int i = 0; i < _inventory.GetInventorySize(); i++)
		{
			ItemSlot itemSlot = GridContainer.GetNode($"ItemSlot{i+1}") as ItemSlot;
			itemSlot.SetItem(_inventory.GetItem(i));
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

	public void AddItem(ItemResource itemResource)
	{
		_inventory.AddItem(itemResource);
		RenderInventory();
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
