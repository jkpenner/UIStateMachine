using UnityEngine;
using System.Collections;

public abstract class UIState : MonoBehaviour {
    public abstract int StateId { get; }

    private UIStateMachine _stateMachine;
    public UIStateMachine StateMachine {
        get { return _stateMachine; }
    }

    public virtual string GetStateName() {
        return "UIState";
    }

    private void Awake() {
        _stateMachine = this.gameObject.GetComponentInParent<UIStateMachine>();
        if (_stateMachine == null) {
            Debug.LogWarning(string.Format(
                "[{0}]: Requires Parent GameObject to have a UIStateMachine Component attached.",
                this.gameObject.name));
        } else {
            _stateMachine.AddState(this);
        }
    }
}
