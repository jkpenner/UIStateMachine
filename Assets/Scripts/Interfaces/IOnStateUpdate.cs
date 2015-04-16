using UnityEngine;
using System.Collections;

/// <summary>
/// Implement this interface on an UIState to recieve notice when the
/// UIState is Updated. Called every time the Monobehaviour.Update method is called.
/// </summary>
public interface IOnStateUpdate {
    void OnUpdate();
}
