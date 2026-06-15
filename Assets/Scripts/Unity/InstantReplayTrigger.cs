using UnityEngine;

namespace Quantum
{
	public class InstantReplayTrigger : MonoBehaviour
	{
		private QuantumInstantReplayDemo _demoReplay;

		private void Start()
		{
		//	QuantumEvent.Subscribe<EventOnDeath>(this, OnDeathEvent);
			_demoReplay = GetComponent<QuantumInstantReplayDemo>();
		}

		void OnDestroy()
		{
			QuantumEvent.UnsubscribeListener(this);
		}

	//	private unsafe void OnDeathEvent(EventOnDeath evt)
	//	{
	//		var frame = evt.Game.Frames.Predicted;
	//
	//		if (frame.Unsafe.TryGetPointer<Player>(evt.Entity, out var player) == false)
	//			return;
	//		if (evt.Game.GetLocalPlayers().Contains(player->PlayerRef) == false)
	//			return;
	//
	//		_demoReplay.Editor_StartInstantReplay();
	//	}
	}
}
