using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITestStateC : UIState, IOnStateAwake, IOnStateEnter, IOnStateExit {
    static public int ID = 3;
    public override int StateId {
        get { return UITestStateC.ID; }
    }

    public override string GetStateName() {
        return "UI Test State C";
    }

    public Animator animator;
    public Button btnSwitchToStateB;

    public void OnAwake() {
        if (btnSwitchToStateB != null) {
            btnSwitchToStateB.onClick.AddListener(OnClickSwitchToStateB);
        }

        this.gameObject.SetActive(false);
        btnSwitchToStateB.interactable = false;
    }

    public void OnEnter() {
        this.gameObject.SetActive(true);        
        animator.Play("UITestStateC_Intro");
    }

    public void OnExit() {
        this.gameObject.SetActive(false);
    }

    private void OnClickSwitchToStateB() {
        btnSwitchToStateB.interactable = false;
        animator.Play("UITestStateC_Exit");
    }

    public void OnIntroAnimationEnd() {
        animator.Play("UITestStateC_Idle");
        btnSwitchToStateB.interactable = true;
    }

    public void OnExitAnimationEnd() {
        StateMachine.PopState();
    }
}
