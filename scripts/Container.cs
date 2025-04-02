using Godot;

public partial class Container : Node3D
{
	[Export] private Interactable Interactable { get; set; }
	[Export] private Inventory Inventory { get; set; }

	public override void _Ready()
	{
		base._Ready();
		Interactable.SetIsInteractable(true);
		Interactable.Interacted += InteractCallback;
	}

    public void InteractCallback()
	{
		GD.Print("Container Interact");
		InventoryManager.GetInstance().SetInventory(Inventory);
		InventoryManager.GetInstance().ToggleInventory();
	}
}
