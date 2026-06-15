namespace Quantum
{
	unsafe partial struct Weapon
	{
		internal void Update(Frame frame, EntityRef entity, in WeaponsFilter filter)
		{
			var transform       = frame.Unsafe.GetPointer<Transform2D>(entity);
			transform->Position = filter.Transform->Position;
			transform->Rotation = filter.Transform->Rotation;

			Timer -= frame.DeltaTime;

			if (filter.Weapons->Desires == true && Timer <= 0)
			{
				ProcessAttack(frame, transform, in filter);
				Timer = AttackTime;
			}

			filter.Weapons->Desires = false;
		}

		private void ProcessAttack(Frame frame, Transform2D* transform, in WeaponsFilter filter)
		{
			var projectileEntity    = frame.Create(Projectile);
			var projectile          = frame.Unsafe.GetPointer<Projectile>(projectileEntity);
			
			projectile->Set(frame, projectileEntity, filter.Entity, transform->Position, transform->Forward, Range);
		}
	}
}
