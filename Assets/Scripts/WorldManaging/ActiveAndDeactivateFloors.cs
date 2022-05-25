using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAndDeactivateFloors:MonoBehaviour {

    public GameObject[] floors;
    public int currentfloor;
    int floorChecker;

    void Start() {
        floorChecker = currentfloor = PlayerPrefs.GetInt("CurrentFloor");
        ChangeCurrentFloor();
    }

    void ChangeCurrentFloor() {
        for(int i = 0; i < floors.Length; i++) {
            if(i == currentfloor) {
                floors[i].SetActive(true);

                if(i != 0) {
                    floors[i - 1].SetActive(true);
                }

            } else {
                floors[i].SetActive(false);
            }
        }
    }

    public void IncreaseCurrentFloor() {
        if(currentfloor == floors.Length)
            return;

        currentfloor++;
        ChangeCurrentFloor();
    }

    public void DecreaseCurrentFloor() {
        if(currentfloor != 0)
            currentfloor--;

        ChangeCurrentFloor();
    }
}
