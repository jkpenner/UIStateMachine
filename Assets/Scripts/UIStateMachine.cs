using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIStateMachine : MonoBehaviour {
    public UIState initialState = null;

    private Stack<UIState> _uiStack;
    private Dictionary<int, UIState> _uiStates;

    #region Private State Methods
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

    
    private void Update() {
        UIState currState = PeekState();
        if (currState != null) {
            OnUpdate(currState);
        }
    }

    private void ValidateStateMachine() {
        if (_uiStack == null) _uiStack = new Stack<UIState>();
        if (_uiStates == null) _uiStates = new Dictionary<int, UIState>();
    }
    #endregion Private State Methods

    #region Public State Methods
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

    public UIState PeekState() {
        ValidateStateMachine();

        if (_uiStack.Count > 0) {
            return _uiStack.Peek();
        }
        return null;
    }

    public void SetState(int stateId) {
        if (ContainState(stateId)) {
            ClearStates();
            PushState(stateId);
        }
    }

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
    private void OnAwake(UIState state) {
        var i = state as IOnStateAwake;
        if (i != null) i.OnAwake();
    }

    private void OnUpdate(UIState state) {
        var i = state as IOnStateUpdate;
        if (i != null) i.OnUpdate();
    }

    private void OnEnter(UIState state) {
        var i = state as IOnStateEnter;
        if (i != null) i.OnEnter();
    }

    private void OnExit(UIState state) {
        var i = state as IOnStateExit;
        if (i != null) i.OnExit();
    }

    private void OnFocus(UIState state) {
        var i = state as IOnStateFocus;
        if (i != null) i.OnFocus();
    }

    private void OnDefocus(UIState state) {
        var i = state as IOnStateDefocus;
        if (i != null) i.OnDefocus();
    }
    #endregion Interface Handling Methods
}
