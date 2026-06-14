namespace Quantum
{
	public unsafe struct ProjectileFilter
	{
		public EntityRef   Entity;
		public Projectile* Projectile;
	}

	internal unsafe sealed class ProjectileSystem : SystemMainThreadFilter<ProjectileFilter>, ISignalOnCollision2D
	{
		public override void Update(Frame frame, ref ProjectileFilter filter)
		{
			filter.Projectile->Update(frame, in filter);
		}

		void ISignalOnCollision2D.OnCollision2D(Frame frame, CollisionInfo2D info)
		{
			if (frame.Unsafe.TryGetPointer<Projectile>(info.Entity, out var projectile) == false)
				return;

			if (projectile->Owner == info.Other)
			{
				info.IgnoreCollision = true;
				return;
			}

			if (frame.Unsafe.TryGetPointer<Health>(info.Other, out var health) == true)
			{
				health->DealDamage(frame, info.Other, 1);
			}

			frame.Destroy(info.Entity);
		}
	}
}
