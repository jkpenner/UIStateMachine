using UnityEngine;
using System.Collections;

/// <summary>
/// The base class for all states that interact with the UIStateMachine
/// </summary>
public abstract class UIState : MonoBehaviour {
    /// <summary>
    /// Gets the StateId
    /// </summary>
    public abstract int StateId { get; }

    private UIStateMachine _stateMachine;
    /// <summary>
    /// Gets the instance of the UIStateMachine controlling the UIState
    /// </summary>
    public UIStateMachine StateMachine {
        get { return _stateMachine; }
    }

    /// <summary>
    /// Gets if the UIState is currently focused by the controlling UIStateMachine
    /// </summary>
    public bool IsFocused {
        get {
            if (_stateMachine != null) {
                if (_stateMachine.PeekState() == this) {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// Get the name of the UIState
    /// </summary>
    public virtual string GetStateName() {
        return "UIState";
    }

    /// <summary>
    /// UIState will get the UIStateMachine component from a parent gameobject, and
    /// automatically add itself to the State Machine, if one exsists.
    /// </summary>
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
