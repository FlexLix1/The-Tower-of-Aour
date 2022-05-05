using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IncreaseOrDecreaseCurrentFloor:MonoBehaviour {

    public int floorNumber;
    bool upperFloor;

    [SerializeField]
    UnityEvent IncreaseCurrentFloor = null;

    [SerializeField]
    UnityEvent DecreaseCurrentFloor = null;

    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player"))
            return;

        if(!upperFloor) {
            upperFloor = true;
            IncreaseCurrentFloor.Invoke();
        } else if(upperFloor) {
            upperFloor = false;
            DecreaseCurrentFloor.Invoke();
        }
    }
}
