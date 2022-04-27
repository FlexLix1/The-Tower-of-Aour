using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact:MonoBehaviour {

    DynamicLever leverScript;
    DynamicDoor doorScript;
    PickUp inventoryScript;
    string saveTag;
    bool onTrigger;

    void Start() {
        inventoryScript = GetComponent<PickUp>();    
    }

    void Update() {
        if(!onTrigger)
            return;

        if(Input.GetKeyDown(KeyCode.E)) {
            UseItem();
        }
    }

    void UseItem() {
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
            case "Door":
                doorScript.openDoor = true;
                inventoryScript.UseItem();
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
            case "Door":
                onTrigger = true;
                saveTag = other.tag;
                doorScript = other.gameObject.GetComponent<DynamicDoor>();
                break;
        }
    }

    void OnTriggerExit(Collider other) {
        if(onTrigger) {
            onTrigger = false;
            saveTag = null;
        }
    }
}
