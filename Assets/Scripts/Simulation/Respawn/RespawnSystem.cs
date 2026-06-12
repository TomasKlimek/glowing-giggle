using Photon.Deterministic;

namespace Quantum
{
	public unsafe struct RespawnFilter
	{
		public EntityRef    Entity;
		public Transform2D* Transform;
		public Respawn*     Respawn;
		public Health*      Health;
	}

	internal unsafe sealed class RespawnSystem : SystemMainThreadFilter<RespawnFilter>
	{
		private static readonly FP MAX_RANGE_SQR = (FP)256;

		public override void Update(Frame frame, ref RespawnFilter filter)
		{
			filter.Respawn->Timer -= frame.DeltaTime;
			if (filter.Respawn->Timer > 0)
				return;

			frame.Remove<Respawn>(filter.Entity);
			filter.Health->Revive();

			filter.Transform->Teleport(frame, GetSpawnPosition(frame));
		}

		private FPVector2 GetSpawnPosition(Frame frame)
		{
			var bestDistance   = FP.Minus_1;
			var spawnPositions = frame.AllocateList<FPVector2>(16);
			var spawnPoints    = frame.Filter<SpawnPoint, Transform2D>();

			while (spawnPoints.NextUnsafe(out _, out _, out var spawnTransform) == true)
			{
				var spawnPointDistance = FP.MaxValue;
				var players            = frame.Filter<Player, Transform2D>();

				while (players.NextUnsafe(out _, out _, out var playerTransform) == true)
				{
					var distance = FPMath.Min(MAX_RANGE_SQR, (playerTransform->Position - spawnTransform->Position).SqrMagnitude);
					if (distance >= spawnPointDistance)
						continue;

					spawnPointDistance = distance;
				}

				if (spawnPointDistance > bestDistance)
				{
					spawnPositions.Clear();
					bestDistance = spawnPointDistance;
				}

				spawnPositions.Add(spawnTransform->Position);
			}

			var result = spawnPositions[frame.RNG->Next(0, spawnPositions.Count - 1)];

			frame.FreeList<FPVector2>(spawnPositions);

			return result;
		}
	}
}
