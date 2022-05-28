using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager:MonoBehaviour {
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

    void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Respawn"))
            return;

        Vector3 loadPosition;
        loadPosition.x = PlayerPrefs.GetFloat("PlayerPosX");
        loadPosition.y = PlayerPrefs.GetFloat("PlayerPosY");
        loadPosition.z = PlayerPrefs.GetFloat("PlayerPosZ");
        transform.position = loadPosition;
        currentFloor.currentfloor = PlayerPrefs.GetInt("CurrentFloor", currentFloor.currentfloor);
    }
}
