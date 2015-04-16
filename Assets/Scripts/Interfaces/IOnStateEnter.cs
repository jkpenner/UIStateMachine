using UnityEngine;
using System.Collections;

/// <summary>
/// Implement this interface on an UIState to recieve notice when the
/// UIState is Entered.
/// </summary>
public interface IOnStateEnter {
    void OnEnter();
}