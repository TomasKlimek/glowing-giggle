using UnityEngine;

namespace Quantum
{
	public class HealthView : QuantumEntityViewComponent
	{
		[SerializeField] Transform _hp;

		private int _lastHP = -1;

		public unsafe override void OnUpdateView()
		{
			var frame = PredictedFrame;
			if (frame == null)
				return;

			if (frame.Unsafe.TryGetPointer<Health>(EntityRef, out var qHealth) == false)
				return;

			if (_lastHP == qHealth->Current)
				return;

			_hp.localScale = (qHealth->Current / (float)qHealth->Max) * Vector3.one;
			_lastHP = qHealth->Current;
		}
	}
}
