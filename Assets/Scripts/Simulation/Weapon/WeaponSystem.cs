namespace Quantum
{
	public unsafe struct WeaponFilter
	{
		public EntityRef    Entity;
		public Weapon*      Weapon;
		public Transform2D* Transform;
	}

	internal unsafe sealed class WeaponSystem : SystemMainThreadFilter<WeaponFilter>
	{
		public override void Update(Frame frame, ref WeaponFilter filter)
		{
			filter.Weapon->Update(frame, in filter);
		}
	}
}
