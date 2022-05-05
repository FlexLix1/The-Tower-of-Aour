using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLever:MonoBehaviour {

    public Elevator elevatorScript;
    public DynamicDoor doorScript;
    public bool leverActive, startTrue, inUse, oneTimeUse;
    public Animator anim;

    

    void Start() {


        if(startTrue) {
            anim.Play("lever_True");
            leverActive = true;
        } else {
            anim.Play("lever_False");
            leverActive = false;
        }
    }

    public void LeverUp() {
        inUse = true;
        anim.Play("lever_MTrue");
        CancelInvoke();
        Invoke(nameof(SetLeverTrue), 1);
    }

    public void LeverDown() {
        inUse = true;
        anim.Play("lever_MFalse");
        CancelInvoke();
        Invoke(nameof(SetLeverFalse), 1);
    }

    void SetLeverFalse() {
        anim.Play("lever_False");
        doorScript.openDoor = false;
        leverActive = false;
        if(oneTimeUse)
            return;
        inUse = false;
    }

    void SetLeverTrue() {
        anim.Play("lever_True");
        doorScript.openDoor = true;
        leverActive = true;
        if(oneTimeUse)
            return;
        inUse = false;
    }
}
