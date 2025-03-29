using System.Collections.Generic;
using Godot;

public partial class Soil : Node3D
{
	public enum SoilStatus
	{
		Soil,
		Tilled,
		Watered
	}

	[Export]
	public Material SoilMaterial { get; set; }

	[Export]
	public Material SoilTilledMaterial { get; set; }

	[Export]
	public Material SoilWateredMaterial { get; set; }

	[Export]
	private SoilStatus SoilStatusStart { get; set; }

	private SoilStatus _soilStatus;
	private Dictionary<SoilStatus, Material> _materialsLookup;
	private CsgBox3D _box3d;

	public override void _Ready()
	{
		base._Ready();
		
		_materialsLookup = new Dictionary<SoilStatus, Material>()
		{
			{ SoilStatus.Soil, SoilMaterial},
			{ SoilStatus.Tilled, SoilTilledMaterial},
			{ SoilStatus.Watered, SoilWateredMaterial}
		};

		_box3d = GetNode("./CSGBox3D") as CsgBox3D;

		SetSoilStatus(SoilStatusStart);
	}

	public void SetSoilStatus(SoilStatus soilStatus)
	{
		_soilStatus = soilStatus;
		_box3d.Material = _materialsLookup[soilStatus];
	}
}
