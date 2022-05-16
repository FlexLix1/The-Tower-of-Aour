using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IncreaseOrDecreaseCurrentFloor:MonoBehaviour {
    bool upperFloor;

    void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player"))
            return;

        ActiveAndDeactivateFloors playerFloorScript = other.GetComponent<ActiveAndDeactivateFloors>();

        if(!upperFloor) {
            playerFloorScript.IncreaseCurrentFloor();
            upperFloor = true;
        } else if(upperFloor) {
            playerFloorScript.DecreaseCurrentFloor();
            upperFloor = false;
        }
    }

    void OnTriggerExit(Collider other) {
        if(!other.CompareTag("Player"))
            return;

        ActiveAndDeactivateFloors playerFloorScript = other.GetComponent<ActiveAndDeactivateFloors>();
        if(!upperFloor) {
            if(other.transform.position.y > transform.position.y) {
                playerFloorScript.IncreaseCurrentFloor();
                upperFloor = true;
            }
        } else if(upperFloor) {
            if(other.transform.position.y < transform.position.y) {
                playerFloorScript.DecreaseCurrentFloor();
                upperFloor = false;
            }
        }
    }
}
