using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {
    ActiveAndDeactivateFloors currentFloor;

    void Start() {
        currentFloor = GetComponent<ActiveAndDeactivateFloors>();    
    }

    public void SavePlayer() {
        PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", transform.position.y + 1);
        PlayerPrefs.SetFloat("PlayerPosZ", transform.position.z);
        PlayerPrefs.SetInt("CurrentFloor", currentFloor.currentfloor);
    }
}
