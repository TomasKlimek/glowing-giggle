namespace Quantum
{
	unsafe partial struct Player
	{
		internal void Update(Frame frame, in PlayerFilter filter)
		{
			Update_Commands(frame, in filter);
			Update_Input(frame, in filter);
		}

		private void Update_Commands(Frame frame, in PlayerFilter filter)
		{
			if (filter.Health->IsAlive == false)
				return;

			var command = frame.GetPlayerCommand(filter.Player->PlayerRef);
			if (command is not IPlayerCommand playerCommand)
				return;

			playerCommand.Process(frame, in filter);
		}

		private void Update_Input(Frame frame, in PlayerFilter filter)
		{
			if (filter.Health->IsAlive == true)
			{
				var input = frame.GetPlayerInput(filter.Player->PlayerRef);

				filter.Movement->Desires = input->Direction;
				filter.Weapons  ->Desires = input->Attack.IsDown;
			}
			else
			{
				filter.Movement->Desires = default;
				filter.Weapons  ->Desires = default;
			}
		}
	}
}
