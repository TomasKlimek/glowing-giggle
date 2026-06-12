using Photon.Deterministic;

namespace Quantum
{
	unsafe partial struct Projectile
	{
		internal void Set(Frame frame, EntityRef entity, EntityRef owner, FPVector2 position, FPVector2 direction)
		{
			Owner = owner;

			var transform = frame.Unsafe.GetPointer<Transform2D>(entity);
			var body      = frame.Unsafe.GetPointer<PhysicsBody2D>(entity);

			transform->Position = position + direction;
			transform->Rotation = FPVector2.RadiansSigned(direction, FPVector2.Right);
			body->Velocity      = Speed * direction;
		}
	}
}
