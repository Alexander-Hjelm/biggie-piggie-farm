using Godot;

public partial class UIManager : Control
{
	public static UIManager _instance { get; private set; }

	public override void _EnterTree()
	{
		base._EnterTree();
		if (_instance == null)
		{
			_instance = this;
		}
		else
		{
			GD.PrintErr("There are more than one UIManager instance in the scene. Make sure that there is only one.");
		}
	}
}
