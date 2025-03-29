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

	[Export]
	private Interactable Interactable { get; set; }

	private SoilStatus _soilStatus;
	private Dictionary<SoilStatus, Material> _materialsLookup;
	private CsgBox3D _box3d;

	[Signal]
    public delegate void IsInteractableChangedEventHandler(bool isInteractable);

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

		Interactable.Interacted += InteractCallback;
		IsInteractableChanged += Interactable.SetIsInteractable;
	}

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
		bool interactable = EvalIsInteractable();
		if(interactable != Interactable.IsInteractable())
		{
			EmitSignal(SignalName.IsInteractableChanged, interactable);
		}
    }

	public bool EvalIsInteractable()
	{
		if (
			_soilStatus == SoilStatus.Soil
			&& Player.GetInstance().CurrentTool.Capabilities.Contains(ToolResource.Capability.TILL)
		)
		{
			return true;
		}
		else if (
			_soilStatus == SoilStatus.Tilled
			&& Player.GetInstance().CurrentTool.Capabilities.Contains(ToolResource.Capability.WATER)
		)
		{
			return true;
		}
		return false;
	}

	public void InteractCallback()
	{
		if (
			_soilStatus == SoilStatus.Soil
			&& Player.GetInstance().CurrentTool.Capabilities.Contains(ToolResource.Capability.TILL)
		)
		{
			SetSoilStatus(SoilStatus.Tilled);
		}
		if (
			_soilStatus == SoilStatus.Tilled
			&& Player.GetInstance().CurrentTool.Capabilities.Contains(ToolResource.Capability.WATER)
		)
		{
			SetSoilStatus(SoilStatus.Watered);
		}
	}

	public void SetSoilStatus(SoilStatus soilStatus)
	{
		_soilStatus = soilStatus;
		_box3d.Material = _materialsLookup[soilStatus];
	}
}
