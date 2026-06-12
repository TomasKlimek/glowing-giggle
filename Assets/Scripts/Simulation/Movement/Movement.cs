using Photon.Deterministic;

namespace Quantum
{
	unsafe partial struct Movement
	{
		internal void Update(Frame frame, in MovementFilter filter)
		{
			if (Desires == default)
			{
				filter.Body->Velocity = default;
				return;
			}

			var direction = Desires.Normalized;

			filter.Body->Velocity      = direction * Speed;
			filter.Transform->Rotation = -FPVector2.RadiansSigned(direction, FPVector2.Right);
		}
	}
}
