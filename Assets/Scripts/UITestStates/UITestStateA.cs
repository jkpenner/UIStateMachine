using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITestStateA : UIState, IOnStateAwake, IOnStateEnter, IOnStateExit {
    static public int ID = 1;
    public override int StateId {
        get { return UITestStateA.ID; }
    }

    public override string GetStateName() {
        return "UI Test State A";
    }

    public Button btnSwitchToStateB;

    public void OnAwake() {
        if (btnSwitchToStateB != null) {
            btnSwitchToStateB.onClick.AddListener(OnClickSwitchToStateB);
        }

        this.gameObject.SetActive(false);
    }

    public void OnEnter() {
        this.gameObject.SetActive(true);
    }

    public void OnExit() {
        this.gameObject.SetActive(false);
    }

    public void OnClickSwitchToStateB() {
        StateMachine.SetState(UITestStateB.ID);
    }
}
