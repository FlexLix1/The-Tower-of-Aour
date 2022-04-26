using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact:MonoBehaviour {

    float rayDistance = 1.5f;

    void Update() {
        if(Input.GetKeyDown(KeyCode.E)) {
            CheckInteraction();
        }
    }

    void CheckInteraction() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward * rayDistance, out hit, rayDistance)) {
            switch(hit.collider.tag) {
                case "Lever":
                    SimpleLever leverScript = hit.collider.gameObject.GetComponent<SimpleLever>();
                    if(leverScript.leverActive) {
                        leverScript.LeverDown();
                    } else {
                        leverScript.LeverUp();
                    }
                    break;
            }
        }
    }
}
