using Godot;

public partial class CameraMovement : Node3D
{
	[Export]
	public Node3D Target { get; set; }

	[Export]
	public float SmoothingFactor { get; set; } = 6.0f;

	public override void _PhysicsProcess(double delta)
	{
		Position = Position.Lerp(Target.Position, (float)delta*SmoothingFactor);
	}
}
