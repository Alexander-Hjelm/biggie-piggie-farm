using Godot;
using biggiepiggiefarm.scripts.groups;

namespace biggiepiggiefarm.scripts
{
	public partial class PlayerInteraction : Node3D
	{
		private RayCast3D _rayCast3D;

		public override void _Ready()
		{
			base._Ready();
			_rayCast3D = GetNode<RayCast3D>("./RayCast3D");
		}


		public override void _PhysicsProcess(double delta)
		{
			if (_rayCast3D.IsColliding())
			{
				CollisionObject3D collider = _rayCast3D.GetCollider() as CollisionObject3D;
				Node3D colliderRootObj = collider.GetParentNode3D();

				if (colliderRootObj.IsInGroup(Groups.INTERACTABLE))
				{
					GD.Print(colliderRootObj);
				}
			}
			else
			{
				GD.Print("Not colliding");
			}
		}
	}
}
