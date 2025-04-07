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
	private SeedResource _plantedSeedResource;
	private Node3D _currentCropScene;
	private double _minutesUntilDry = 1440;
	private double _minutesGrown = 0;
	private double _minutesGrownForStateChange = 2;
	private int _currentCropStage = -1;

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

        CheckWatered(delta);

		if(_minutesGrown >= _minutesGrownForStateChange
			&& _currentCropStage < _plantedSeedResource.Stages.Count - 1)
		{
			_currentCropStage += 1;
			ChangeCropScene(_currentCropStage);
			_minutesGrown = 0;
		}
    }

    private void CheckWatered(double delta)
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
			_minutesGrown += delta;
        }
    }

    public bool EvalIsInteractable()
    {
        return
			IsInteractableForSoilType(SoilStatus.Soil, ToolResource.Capability.TILL)
			|| IsInteractableForSoilType(SoilStatus.Tilled, ToolResource.Capability.WATER)
			|| IsInteractablePlayerHoldingSeed();
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
		else if (IsInteractablePlayerHoldingSeed())
		{
			PlantSeed(Player.GetInstance().CurrentItem as SeedResource);
		}
	}

	private bool IsInteractableForSoilType(SoilStatus soilStatus, ToolResource.Capability capability)
    {
		ToolResource currentTool = Player.GetInstance().CurrentItem as ToolResource;
        return currentTool != null
			&& _soilStatus == soilStatus
			&& currentTool.Capabilities.Contains(capability);
    }

	private bool IsInteractablePlayerHoldingSeed()
    {
        return (_soilStatus == SoilStatus.Tilled || _soilStatus == SoilStatus.Watered)
			&& Player.GetInstance().CurrentItem as SeedResource != null;
    }

	public void SetSoilStatus(SoilStatus soilStatus)
	{
		_soilStatus = soilStatus;
		_box3d.Material = _materialsLookup[soilStatus];
	}

	private void PlantSeed(SeedResource seedResource)
	{
		_plantedSeedResource = seedResource;
		_currentCropStage = 0;
		ChangeCropScene(0);
	}

	private void ChangeCropScene(int cropStateIndex)
	{
		if(_currentCropScene != null)
		{
			_currentCropScene.QueueFree();
		}
		_currentCropScene = _plantedSeedResource.Stages[cropStateIndex].Instantiate() as Node3D;
		AddChild(_currentCropScene);
		_currentCropScene.Position += new Vector3(0f, .5f, 0f);
	}
}
