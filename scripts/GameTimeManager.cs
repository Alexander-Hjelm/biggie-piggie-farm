using Godot;

public partial class GameTimeManager : Node3D
{
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
        gameTimeStamp = new GameTimeStamp(1, GameTimeStamp.Season.Spring, 1, 6, 0);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        Tick(delta*gameTimeScale);
    }

    private void Tick(double delta)
    {
        gameTimeStamp.UpdateClock(delta);
        GD.Print(gameTimeStamp);
    }
}