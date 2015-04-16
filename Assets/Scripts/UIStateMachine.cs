using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIStateMachine : MonoBehaviour {
    /// <summary>
    /// The UIState the UIStateMachine will start on
    /// </summary>
    public UIState initialState = null;

    /// <summary>
    /// The Stack of UIStates
    /// </summary>
    private Stack<UIState> _uiStack;

    /// <summary>
    /// All UIStates being controlled by the UIStateMachine
    /// </summary>
    private Dictionary<int, UIState> _uiStates;

    #region Private State Methods
    /// <summary>
    /// Validates the StateMachine and then Pushes the initialState to be the current state
    /// </summary>
    private void Start() {
        ValidateStateMachine();
        if (initialState != null) {
            if (_uiStates.ContainsKey(initialState.StateId)) {
                PushState(initialState.StateId);
            } else {
                Debug.LogWarning(string.Format(
                    "[{0}]: Initial State value is not childed to the UIStateMachine",
                    this.gameObject.name));
            }
        }
    }

    /// <summary>
    /// Gets the current state and attempts to call the Update Method
    /// </summary>
    private void Update() {
        UIState currState = PeekState();
        if (currState != null) {
            OnUpdate(currState);
        }
    }

    /// <summary>
    /// Validates that the _uiStack and _uiStates are not null
    /// </summary>
    private void ValidateStateMachine() {
        if (_uiStack == null) _uiStack = new Stack<UIState>();
        if (_uiStates == null) _uiStates = new Dictionary<int, UIState>();
    }
    #endregion Private State Methods

    #region Public State Methods
    /// <summary>
    /// Pushed the UIState with the passed stateId ontop of the _uiStack.
    /// Calls the corrisponding interface events for the previous and next state
    /// </summary>
    /// <param name="stateId">UIState to be pushed on top of the stack</param>
    public void PushState(int stateId) {
        ValidateStateMachine();

        if (!ContainState(stateId)) return;

        UIState prevState = PeekState();
        if (prevState != null) {
            OnDefocus(prevState);
        }

        UIState nextState = _uiStates[stateId];
        if (nextState != null) {
            _uiStack.Push(nextState);
            OnEnter(nextState);
            OnFocus(nextState);
        }
    }

    /// <summary>
    /// Pops the top most UIState from the _uiStack. Calls the corrisponding 
    /// interface events for the previous and next state.
    /// </summary>
    public void PopState() {
        ValidateStateMachine();

        if (_uiStack.Count == 0) return;

        UIState prevState = _uiStack.Pop();
        if (prevState != null) {
            OnDefocus(prevState);
            OnExit(prevState);
        }

        UIState nextState = PeekState();
        if (nextState != null) {
            OnFocus(nextState);
        }
    }

    /// <summary>
    /// Gets the topmost UIState from the _uiStack. This state is the
    /// currently active state.
    /// </summary>
    /// <returns>Returns the Current UIState</returns>
    public UIState PeekState() {
        ValidateStateMachine();

        if (_uiStack.Count > 0) {
            return _uiStack.Peek();
        }
        return null;
    }

    /// <summary>
    /// Removes all UIStates from the _uiStack and then Pushes the UIState 
    /// with the passed stateId.
    /// </summary>
    /// <param name="stateId"></param>
    public void SetState(int stateId) {
        if (ContainState(stateId)) {
            ClearStates();
            PushState(stateId);
        }
    }

    /// <summary>
    /// Removes all UIStates from the _uiStack. Calls both the Defocus and 
    /// Exit events for the top most UIState, and the Exit event for the rest.
    /// </summary>
    public void ClearStates() {
        ValidateStateMachine();

        UIState prevState = PeekState();
        if (prevState != null) {
            OnDefocus(prevState);
        }

        while (_uiStack.Count > 0) {
            UIState tempState = _uiStack.Pop();
            if (tempState != null) {
                OnExit(tempState);
            }
        }

        _uiStack.Clear();
    }

    /// <summary>
    /// Checks to see if the UIStateMachine is controlling a UIState with the passed stateId
    /// </summary>
    public bool ContainState(int stateId) {
        ValidateStateMachine();
        if (!_uiStates.ContainsKey(stateId)) {
            Debug.LogWarning(string.Format(
                "[{0}]: Pushed StateId: {1} does not existed in the UIStateMachine.",
                this.gameObject.name, stateId));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Adds a UIState to the UIStateMachine, called by the UIState base classs.
    /// Calls the OnAwake event once UIState is added.
    /// </summary>
    /// <param name="state"></param>
    public void AddState(UIState state) {
        ValidateStateMachine();
        if (state == null) {
            Debug.LogWarning(string.Format(
                "[{0}]: Adding null state to UIStateMachine.",
                this.gameObject.name));
            return;
        }

        if (!_uiStates.ContainsKey(state.StateId)) {
            _uiStates.Add(state.StateId, state);
        }

        OnAwake(state);
    }
    #endregion Public State Methods

    #region Interface Handling Methods
    /// <summary>
    /// Calls the OnAwake Method if the UIState implements the IOnStateAwake interface
    /// </summary>
    private void OnAwake(UIState state) {
        var i = state as IOnStateAwake;
        if (i != null) i.OnAwake();
    }

    /// <summary>
    /// Calls the OnUpdate Method if the UIState implements the IOnStateUpdate interface
    /// </summary>
    private void OnUpdate(UIState state) {
        var i = state as IOnStateUpdate;
        if (i != null) i.OnUpdate();
    }

    /// <summary>
    /// Calls the OnEnter Method if the UIState implements the IOnStateEnter interface
    /// </summary>
    private void OnEnter(UIState state) {
        var i = state as IOnStateEnter;
        if (i != null) i.OnEnter();
    }

    /// <summary>
    /// Calls the OnExit Method if the UIState implements the IOnStateExit interface
    /// </summary>
    private void OnExit(UIState state) {
        var i = state as IOnStateExit;
        if (i != null) i.OnExit();
    }

    /// <summary>
    /// Calls the OnFocus Method if the UIState implements the IOnStateFocus interface
    /// </summary>
    private void OnFocus(UIState state) {
        var i = state as IOnStateFocus;
        if (i != null) i.OnFocus();
    }

    /// <summary>
    /// Calls the OnDefocus Method if the UIState implements the IOnStateDefocus interface
    /// </summary>
    private void OnDefocus(UIState state) {
        var i = state as IOnStateDefocus;
        if (i != null) i.OnDefocus();
    }
    #endregion Interface Handling Methods
}
