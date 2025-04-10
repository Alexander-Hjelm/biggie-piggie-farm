using Godot;

public partial class Player : CharacterBody3D
{
	[Export] public Inventory Inventory { get; private set; }
	// How fast the player moves in meters per second.
	[Export] int Speed { get; set; } = 5;
	[Export] public ItemResource CurrentItem { get; private set; }

	[Signal] public delegate void CurrentItemChangedEventHandler(ItemResource item);

	private static Player _instance;
	private Vector3 _targetVelocity = Vector3.Zero;

	public static Player GetInstance()
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
			GD.PrintErr("There are more than one Player instance in the scene. Make sure that there is only one.");
		}
    }

    public override void _Ready()
    {
        base._Ready();

		// Ensure that the CurrentToolChanged signal is emitted on game start
		if (CurrentItem != null)
		{
			SetCurrentItem(CurrentItem);
		}
    }

	public override void _PhysicsProcess(double delta)
	{
		// We create a local variable to store the input direction.
		var direction = Vector3.Zero;

		// We check for each move input and update the direction accordingly.
		// Notice how we are working with the vector's X and Z axes.
		// In 3D, the XZ plane is the ground plane.
		if (Input.IsActionPressed("move_right"))
		{
			direction.X += 1.0f;
		}
		if (Input.IsActionPressed("move_left"))
		{
			direction.X -= 1.0f;
		}
		if (Input.IsActionPressed("move_back"))
		{
			direction.Z += 1.0f;
		}
		if (Input.IsActionPressed("move_forward"))
		{
			direction.Z -= 1.0f;
		}
		
		if (direction != Vector3.Zero)
		{
			direction = direction.Normalized();
			// Setting the basis property will affect the rotation of the node.
			//GetNode<CharacterBody3D>(".").Basis = Basis.LookingAt(direction);
			Basis = Basis.LookingAt(direction);
		}
		
		// Ground velocity
		_targetVelocity.X = direction.X * Speed;
		_targetVelocity.Z = direction.Z * Speed;

		// Vertical velocity
		//if (!IsOnFloor()) // If in the air, fall towards the floor. Literally gravity
		//{
		//	_targetVelocity.Y -= FallAcceleration * (float)delta;
		//}

		// Moving the character
		Velocity = _targetVelocity;
		MoveAndSlide();

		// Open/Close inventory
		if (Input.IsActionJustPressed("inventory"))
		{
			PlayerInventoryManager inventoryManager = PlayerInventoryManager.GetInstance();
			inventoryManager.ToggleInventory();
		}
	}

	public void SetCurrentItem(ItemResource item)
	{
		CurrentItem = item;
		EmitSignal(SignalName.CurrentItemChanged, item);
	}
}
