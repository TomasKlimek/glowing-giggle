namespace Quantum
{
	unsafe partial struct Weapons
	{
		internal void Update(Frame frame, in WeaponsFilter filter)
		{
			if (frame.Unsafe.TryGetPointer<Weapon>(ActiveWeapon, out var weapon) == true)
			{
				weapon->Update(frame, ActiveWeapon, in filter);
			}
		}
	}
}
