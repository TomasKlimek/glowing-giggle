namespace Quantum {
  using Photon.Deterministic;
  using UnityEngine;

  /// <summary>
  /// A Unity script that creates empty input for any Quantum game.
  /// </summary>
  public class QuantumDebugInput : MonoBehaviour {

    [SerializeField] Input _lastInput;

    private bool _attackPressed;

    private void OnEnable() {
      QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
    }

    private void Update() {
      _attackPressed |= UnityEngine.Input.GetKey(KeyCode.Space);
    }

    /// <summary>
    /// Set an empty input when polled by the simulation.
    /// </summary>
    /// <param name="callback"></param>
    public void PollInput(CallbackPollInput callback) {
#if DEBUG
      if (callback.IsInputSet) {
        Debug.LogWarning($"{nameof(QuantumDebugInput)}.{nameof(PollInput)}: Input was already set by another user script, unsubscribing from the poll input callback. Please delete this component.", this);
        QuantumCallback.UnsubscribeListener(this);
        return;
      }
#endif

      Quantum.Input i = new Quantum.Input();

      var horizontal = UnityEngine.Input.GetAxis("Horizontal");
      var vertical  = UnityEngine.Input.GetAxis("Vertical");


      var directon = new FPVector2
      {
        X = horizontal > 0 ? FP._1 : horizontal < 0 ? FP.Minus_1 : 0,
        Y = vertical   > 0 ? FP._1 : vertical   < 0 ? FP.Minus_1 : 0,
      };

      i.Direction = directon;
      i.Attack    = _attackPressed;

      callback.SetInput(i, DeterministicInputFlags.Repeatable);

      _lastInput     = i;
      _attackPressed = false;
    }
  }
}
