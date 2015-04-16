using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITestStateB : UIState, IOnStateAwake, IOnStateEnter, IOnStateExit, IOnStateFocus, IOnStateDefocus {
    static public int ID = 2;
    public override int StateId {
        get { return UITestStateB.ID; }
    }

    public Button btnSwitchToStateA;
    public Button btnSwitchToStateC;

    public override string GetStateName() {
        return "UI Test State B";
    }

    public void OnAwake() {
        if (btnSwitchToStateA != null) {
            btnSwitchToStateA.onClick.AddListener(OnClickSwitchToStateA);
        }

        if (btnSwitchToStateC != null) {
            btnSwitchToStateC.onClick.AddListener(OnClickSwitchToStateC);
        }

        this.gameObject.SetActive(false);
    }

    public void OnEnter() {
        this.gameObject.SetActive(true);
    }

    public void OnExit() {
        this.gameObject.SetActive(false);
    }

    public void OnFocus() {
        ToggleInteractivity(true);
    }

    public void OnDefocus() {
        ToggleInteractivity(false);
    }

    private void ToggleInteractivity(bool value) {
        btnSwitchToStateA.interactable = value;
        btnSwitchToStateC.interactable = value;
    }

    private void OnClickSwitchToStateA() {
        StateMachine.SetState(UITestStateA.ID);
    }

    private void OnClickSwitchToStateC() {
        StateMachine.PushState(UITestStateC.ID);
    }
}
