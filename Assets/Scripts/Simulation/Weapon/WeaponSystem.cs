namespace Quantum
{
	public unsafe struct WeaponsFilter
	{
		public EntityRef    Entity;
		public Weapons*     Weapons;
		public Transform2D* Transform;
	}

	internal unsafe sealed class WeaponSystem : SystemMainThreadFilter<WeaponsFilter>
	{
		public override void Update(Frame frame, ref WeaponsFilter filter)
		{
			filter.Weapons->Update(frame, in filter);
		}
	}
}
