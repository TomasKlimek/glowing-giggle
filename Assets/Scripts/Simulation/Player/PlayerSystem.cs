using Photon.Deterministic;

namespace Quantum
{
	public unsafe struct PlayerFilter
	{
		public EntityRef Entity;
		public Player*   Player;
		public Movement* Movement;
		public Weapons*  Weapons;
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

			var weapons    = frame.Unsafe.GetPointer<Weapons>(playerEntity);
			var weaponList = frame.AllocateList<EntityRef>(playerData.Weapons.Length);

			weapons->WeaponList = weaponList;

			for (int idx = 0; idx < playerData.Weapons.Length; idx++)
			{
				var weaponEntity    = frame.Create(playerData.Weapons[idx]);
				var weaponTransform = frame.Unsafe.GetPointer<Transform2D>(weaponEntity);

				weaponTransform->Position = FPVector2.Up * 1000;

				weaponList.Add(weaponEntity);

				if (idx == 0)
				{
					weapons->ActiveWeapon = weaponEntity;
				}
			}

			frame.Set(playerEntity, new Respawn());
			frame.Unsafe.GetPointer<Transform2D>(playerEntity)->Position = FPVector2.Up * 1000;
		}
	}
}
