namespace Quantum
{
	public unsafe struct MovementFilter
	{
		public EntityRef      Entity;
		public Movement*      Movement;
		public Transform2D*   Transform;
		public PhysicsBody2D* Body;
	}

	internal unsafe sealed class MovementSystem : SystemMainThreadFilter<MovementFilter>
	{
		public override void Update(Frame frame, ref MovementFilter filter)
		{
			filter.Movement->Update(frame, in filter);
		}
	}
}
