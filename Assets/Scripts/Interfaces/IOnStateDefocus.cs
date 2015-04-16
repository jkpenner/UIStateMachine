using UnityEngine;
using System.Collections;

/// <summary>
/// Implement this interface on an UIState to recieve notice when the
/// UIState becomes out of focus.
/// </summary>
public interface IOnStateDefocus {
    void OnDefocus();
}