using Godot;

public partial class AnimationPlayer : Godot.AnimationPlayer
{
	public override void _Ready()
	{
		base._Ready();

		string[] animations = GetAnimationList();
		Animation animation = GetAnimation(animations[0]);
		animation.LoopMode = Animation.LoopModeEnum.Linear;
	}
}
