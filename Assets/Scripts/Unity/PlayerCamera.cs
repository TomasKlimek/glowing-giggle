using UnityEngine;

namespace Quantum
{
	public class PlayerCamera : MonoBehaviour
	{
		[Range(0f, 10f)]
		[SerializeField] float _lerpSpeed;

		private Vector3 _defaultOffset;

		private void Start()
		{
			_defaultOffset = transform.position;
		}

		private unsafe void Update()
		{
			var game = QuantumRunner.Default?.Game;
			if (game == null)
				return;

			var frame = game.Frames.Predicted;
			if (frame == null)
				return;

			var localPlayers = game.GetLocalPlayers();
			if (localPlayers.Count == 0)
				return;

			var localPlayer = localPlayers[0];
			var players     = frame.Filter<Player, Transform2D>();

			while (players.NextUnsafe(out _, out var player, out var playerTransform) == true)
			{
				if (player->PlayerRef != localPlayer)
					continue;

				transform.position = Vector3.Lerp(transform.position, _defaultOffset + playerTransform->Position.ToUnityVector3(), _lerpSpeed * Time.deltaTime);
			}
		}
	}
}
