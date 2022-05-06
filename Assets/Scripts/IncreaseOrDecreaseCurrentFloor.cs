using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IncreaseOrDecreaseCurrentFloor:MonoBehaviour {
    bool upperFloor;

    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player"))
            return;

        ActiveAndDeactivateFloors playerFloorScript = other.GetComponent<ActiveAndDeactivateFloors>();

        if(!upperFloor) {
            upperFloor = true;
            playerFloorScript.IncreaseCurrentFloor();
        } else if(upperFloor) {
            upperFloor = false;
            playerFloorScript.DecreaseCurrentFloor();
        }
    }
}
