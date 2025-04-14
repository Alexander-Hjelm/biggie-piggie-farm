using Godot;

[GlobalClass]
public partial class StageResource : Resource
{
    [Export] public PackedScene Scene;
    [Export] public int StepsToAdvance = 1;
}