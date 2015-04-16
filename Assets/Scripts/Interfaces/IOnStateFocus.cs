using UnityEngine;
using System.Collections;

/// <summary>
/// Implement this interface on an UIState to recieve notice when the
/// UIState becomes focused.
/// </summary>
public interface IOnStateFocus {
    void OnFocus();
}