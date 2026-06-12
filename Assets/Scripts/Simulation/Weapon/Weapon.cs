namespace Quantum
{
	unsafe partial struct Weapon
	{
		internal void Update(Frame frame, in WeaponFilter filter)
		{
			Timer -= frame.DeltaTime;

			if (Desires == true && Timer <= 0)
			{
				ProcessAttack(frame, in filter);
				Timer = AttackTime;
			}

			Desires = false;
		}

		private void ProcessAttack(Frame frame, in WeaponFilter filter)
		{
			var projectileEntity    = frame.Create(Projectile);
			var projectile          = frame.Unsafe.GetPointer<Projectile>(projectileEntity);
			
			projectile->Set(frame, projectileEntity, filter.Entity, filter.Transform->Position, filter.Transform->Forward);
		}
	}
}
