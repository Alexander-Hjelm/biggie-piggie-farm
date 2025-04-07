using Godot;

public partial class Crop : Node3D
{
    private SeedResource _seedResource;

    public void Initialize(SeedResource seedResource)
    {
        _seedResource = seedResource;
    }
}
