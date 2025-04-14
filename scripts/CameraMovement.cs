using Godot;

public partial class CameraMovement : Node3D
{
	[Export] public Node3D Target { get; set; }
	[Export] public float SmoothingFactor { get; set; } = 6.0f;
	[Export] public float RotationSensitivity { get; set; } = 1.0f;
	[Export] public float RotationYMin { get; set; } = -Mathf.Pi/8;
	[Export] public float RotationYMax { get; set; } = Mathf.Pi/8;

    public override void _Ready()
    {
        base._Ready();
        RotationOrder = EulerOrder.Yxz;
    }

	public override void _Process(double delta)
	{
		Position = Position.Lerp(Target.Position, (float)delta*SmoothingFactor);
	}

	public override void _Input(InputEvent motionUnknown) {
	InputEventMouseMotion motion = motionUnknown as InputEventMouseMotion;
	if (motion != null) {
		float x = motion.Relative.X;
		float y = motion.Relative.Y;
		if (Input.IsActionPressed("camera_rotate"))
		{
			float rotX = x * 0.005f * RotationSensitivity;
			float rotY = y * 0.005f * RotationSensitivity;
			Rotation = new Vector3(
				Mathf.Clamp(Rotation.X + rotY, RotationYMin, RotationYMax),
				Rotation.Y + rotX,
				Rotation.Z
			);
		}
		Vector3 playerNewforward = -Basis.Z;
		playerNewforward.Y = 0;
		Player.GetInstance().SetForward(playerNewforward);
	}
}
}
