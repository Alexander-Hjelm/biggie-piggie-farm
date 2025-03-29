using Godot;
using biggiepiggiefarm.scripts.groups;

namespace biggiepiggiefarm.scripts
{
	public partial class PlayerInteraction : Node3D
	{

		[Export]
		public PackedScene Highlight { get; set; }

		private RayCast3D _rayCast3D;
		private Node3D _highlightInstance;
		private Interactable _selectedInteractable;

		public override void _Ready()
		{
			base._Ready();
			_rayCast3D = GetNode<RayCast3D>("./RayCast3D");
			_highlightInstance = Highlight.Instantiate<Node3D>();
			GetWindow().CallDeferred("add_child", _highlightInstance);
		}

		public override void _PhysicsProcess(double delta)
		{
			_highlightInstance.Visible = false;
			_selectedInteractable = null;
			if (_rayCast3D.IsColliding())
			{
				CollisionObject3D collider = _rayCast3D.GetCollider() as CollisionObject3D;
				Node3D colliderRootObj = collider.GetParentNode3D();

				if (colliderRootObj.IsInGroup(Groups.INTERACTABLE))
				{
					Interactable interactable = colliderRootObj.GetNode("./Interactable") as Interactable;
					if(interactable.IsInteractable())
					{
						_highlightInstance.Position = colliderRootObj.Position + new Vector3(0, 0.5f, 0);
						_highlightInstance.Visible = true;
						_selectedInteractable = interactable;
					}
				}
			}

			if (
				Input.IsActionJustPressed("interact")
				&&_selectedInteractable != null
				&& _selectedInteractable.IsInteractable()
			)
			{
				_selectedInteractable.Interact();
			}
		}
	}
}
