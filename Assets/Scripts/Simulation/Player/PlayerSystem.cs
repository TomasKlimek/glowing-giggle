using Photon.Deterministic;

namespace Quantum
{
	public unsafe struct PlayerFilter
	{
		public EntityRef Entity;
		public Player*   Player;
		public Movement* Movement;
		public Weapon*   Weapon;
		public Health*   Health;
	}

	internal unsafe sealed class PlayerSystem : SystemMainThreadFilter<PlayerFilter>, ISignalOnPlayerAdded
	{
		public override void Update(Frame frame, ref PlayerFilter filter)
		{
			filter.Player->Update(frame, in filter);
		}

		void ISignalOnPlayerAdded.OnPlayerAdded(Frame frame, PlayerRef playerRef, bool firstTime)
		{
			if (firstTime == false)
				return;

			var playerData   = frame.GetPlayerData(playerRef);
			var playerEntity = frame.Create(playerData.PlayerAvatar);
			var player       = frame.Unsafe.GetPointer<Player>(playerEntity);

			player->PlayerRef = playerRef;

			frame.Set(playerEntity, new Respawn());

			frame.Unsafe.GetPointer<Transform2D>(playerEntity)->Position = FPVector2.Up * 1000;
		}
	}
}
