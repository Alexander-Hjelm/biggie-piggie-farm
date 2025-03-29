using Godot;

public partial class PlayerInteraction : Node3D
{
	private RayCast3D _rayCast3D;

	public override void _Ready()
	{
		base._Ready();
		_rayCast3D = GetNode<RayCast3D>("./RayCast3D");
	}


	public override void _PhysicsProcess(double delta)
	{
		if (_rayCast3D.IsColliding())
		{
			CollisionObject3D collider = _rayCast3D.GetCollider() as CollisionObject3D;
			GD.Print(collider);
		}
		else
		{
			GD.Print("Not colliding");
		}
	}
}
