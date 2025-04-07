using Godot;

[GlobalClass]
public partial class ToolResource : ItemResource
{
    public enum Capability {
        TILL,
        WATER
    }

    [Export] public Godot.Collections.Array<Capability> Capabilities { get; set; }
    [Export] public PackedScene GameModel { get; set; }
}
