using UnityEngine;
using System.Collections;

/// <summary>
/// Implement this interface on an UIState to recieve notice when the
/// UIState is exited.
/// </summary>
public interface IOnStateExit {
    void OnExit();
}
