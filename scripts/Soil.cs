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

	[Export] public Material SoilMaterial { get; set; }
	[Export] public Material SoilTilledMaterial { get; set; }
	[Export] public Material SoilWateredMaterial { get; set; }
	[Export] private SoilStatus SoilStatusStart { get; set; }
	[Export] private Interactable Interactable { get; set; }

	[Signal] public delegate void IsInteractableChangedEventHandler(bool isInteractable);

	private SoilStatus _soilStatus;
	private Dictionary<SoilStatus, Material> _materialsLookup;
	private CsgBox3D _box3d;
	private GameTimeStamp _timeWatered;
	private double _minutesUntilDry = 1440;

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

    public override void _Process(double delta)
    {
        base._Process(delta);
        bool interactable = EvalIsInteractable();
        if (interactable != Interactable.IsInteractable())
        {
            EmitSignal(SignalName.IsInteractableChanged, interactable);
        }

        CheckWatered();
    }

    private void CheckWatered()
    {
        if (_soilStatus == SoilStatus.Watered)
        {
            if (GameTimeStamp.DifferenceInMinutes(
                _timeWatered,
                GameTimeManager.GetInstance().gameTimeStamp
            ) > _minutesUntilDry)
            {
                SetSoilStatus(SoilStatus.Tilled);
            }
        }
    }

    public bool EvalIsInteractable()
    {
        return
			IsInteractableForSoilType(SoilStatus.Soil, ToolResource.Capability.TILL)
			|| IsInteractableForSoilType(SoilStatus.Tilled, ToolResource.Capability.WATER);
    }

    public void InteractCallback()
	{
		if (IsInteractableForSoilType(SoilStatus.Soil, ToolResource.Capability.TILL))
		{
			SetSoilStatus(SoilStatus.Tilled);
		}
		else if (IsInteractableForSoilType(SoilStatus.Tilled, ToolResource.Capability.WATER))
		{
			SetSoilStatus(SoilStatus.Watered);
			_timeWatered = GameTimeManager.GetInstance().gameTimeStamp.Clone();
		}
	}

	private bool IsInteractableForSoilType(SoilStatus soilStatus, ToolResource.Capability capability)
    {
        return _soilStatus == soilStatus
			&& Player.GetInstance().CurrentTool.Capabilities.Contains(capability);
    }

	public void SetSoilStatus(SoilStatus soilStatus)
	{
		_soilStatus = soilStatus;
		_box3d.Material = _materialsLookup[soilStatus];
	}
}
