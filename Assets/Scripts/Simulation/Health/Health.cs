namespace Quantum
{
	unsafe partial struct Health
	{
		public bool IsAlive => Current > 0;

		public void DealDamage(Frame frame, EntityRef entity, int value)
		{
			Current -= value;

			if (Current > 0)
				 return;

			if (RespawnTime >= 0)
			{
				frame.Set(entity, new Respawn { Timer = RespawnTime });
			}
			else
			{
				frame.Destroy(entity);
			}
		}

		public void Revive()
		{
			Current = Max;
		}
	}
}
