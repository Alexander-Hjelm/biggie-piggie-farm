using System;
using Godot;

public partial class Sun : Node3D
{
    [Export] public DirectionalLight3D directionalLight3D;
    [Export] public Gradient gradient;

    private GameTimeStamp gameTimeStamp;

    public override void _Ready()
    {
        base._Ready();
        gameTimeStamp = GameTimeManager.GetInstance().gameTimeStamp;
    }


    public override void _Process(double delta)
    {
        base._Process(delta);
        float gameTimeInMinutes = (float)(GameTimeStamp.HoursToMinutes(gameTimeStamp.hour) + gameTimeStamp.minute);
        float newAngle = (float)(.25f * (float)gameTimeInMinutes * Math.PI / 180f) - (float)Math.PI;
        directionalLight3D.Rotation = new Vector3(0, newAngle, 0);
        directionalLight3D.LightColor = gradient.Sample(gameTimeInMinutes/(60f*24f));
    }
}