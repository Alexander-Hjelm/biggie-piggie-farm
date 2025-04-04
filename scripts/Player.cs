using Godot;

public partial class Player : CharacterBody3D
{
	// How fast the player moves in meters per second.
	[Export]
	public int Speed { get; set; } = 5;

	private Vector3 _targetVelocity = Vector3.Zero;
	
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
			this.Basis = Basis.LookingAt(direction);
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
	}
	
}
