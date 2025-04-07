using Godot;

[GlobalClass]
public partial class SeedResource : ItemResource
{
    [Export] public ItemResource CropToYield { get; set; }
    [Export] public int DaysToGrow { get; set; }
    [Export] public Godot.Collections.Array<PackedScene> Stages { get; set; }
}
