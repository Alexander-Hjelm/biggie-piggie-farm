using System;
using Godot;

public partial class Sun : Node3D
{
    [Export] public DirectionalLight3D directionalLight3D;
    [Export] public Gradient gradient;

    private float targetAngle = 0f;

    public override void _Ready()
    {
        base._Ready();
        GameTimeManager.GetInstance().OnTick += HandleGameTimeTick;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        float newAngle = Mathf.Lerp(
            directionalLight3D.Rotation.Y,
            (float)(targetAngle * Math.PI / 180f) - (float)Math.PI,
            (float)delta*.1f
        );
        directionalLight3D.Rotation = new Vector3(0, newAngle, 0);
    }

    private void HandleGameTimeTick(
            int year,
            int season,
            int day,
            int hour,
            int minute
    )
    {
        int gameTimeInMinutes = GameTimeStamp.HoursToMinutes(hour) + minute;
        targetAngle = .25f * gameTimeInMinutes;
        directionalLight3D.LightColor = gradient.Sample(gameTimeInMinutes/(60f*24f));
    }
}