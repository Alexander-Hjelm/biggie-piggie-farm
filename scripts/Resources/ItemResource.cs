using Godot;

[GlobalClass]
public partial class ItemResource : Resource
{
    [Export] public string Name { get; set; }
    [Export(PropertyHint.MultilineText)] public string Description { get; set; }
    [Export] public int Cost { get; set; }
    [Export] public Texture2D Icon { get; set; }
}
