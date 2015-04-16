using UnityEngine;
using System.Collections;

/// <summary>
/// Implement this interface on an UIState to recieve notice when the
/// UIState is added to the UIStateMachine. This call only occurs once, and
/// should be used for initialization of the UIState.
/// </summary>
public interface IOnStateAwake {
    void OnAwake();
}