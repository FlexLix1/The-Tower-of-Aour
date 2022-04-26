using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact:MonoBehaviour {

    DynamicLever leverScript;
    string saveTag;
    bool onTrigger;

    void Update() {
        if(!onTrigger)
            return;

        if(Input.GetKeyDown(KeyCode.E)) {
            Activate();
        }
    }

    void Activate() {
        switch(saveTag) {
            case "Lever":
                if(leverScript.inUse)
                    return;

                if(leverScript.leverActive) {
                    leverScript.LeverDown();
                } else {
                    leverScript.LeverUp();
                }
                break;
        }
    }

    void OnTriggerEnter(Collider other) {
        switch(other.tag) {
            case "Lever":
                onTrigger = true;
                saveTag = other.tag;
                leverScript = other.gameObject.GetComponent<DynamicLever>();
                break;
        }
    }

    void OnTriggerExit(Collider other) {
        if(onTrigger)
            onTrigger = false;
    }
}
