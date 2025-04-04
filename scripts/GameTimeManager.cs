using Godot;

public partial class GameTimeManager : Node3D
{
    [Export] public Timer timer;
    [Export] public float gameTimeScale = 1f;

    public GameTimeStamp gameTimeStamp;
    
    private static GameTimeManager _instance;

	public static GameTimeManager GetInstance()
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
			GD.PrintErr("There are more than one GameTimeManager instance in the scene. Make sure that there is only one.");
		}
        timer.Timeout += Tick;
        timer.OneShot = false;
        timer.WaitTime = gameTimeScale;
    }

    public override void _Ready()
    {
        base._Ready();
        gameTimeStamp = new GameTimeStamp(1, GameTimeStamp.Season.Spring, 1, 6, 0);
        timer.Start();
    }

    private void Tick()
    {
        gameTimeStamp.UpdateClock();
    }
}