using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorInventory:MonoBehaviour {

    GameObject saveItem;
    public GameObject[] inventory;
    public int inventoryActiveNumber;
    bool playerOnElevator;

    public void ActivateItem(int number) {
        inventoryActiveNumber = number;
        inventory[inventoryActiveNumber].SetActive(true);
        if(!playerOnElevator)
            Destroy(saveItem.gameObject);
    }

    public void DeactivateItem() {
        inventory[inventoryActiveNumber].SetActive(false);
    }

    //0 = key, 1 = sparkplug
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
            playerOnElevator = true;

        switch(other.tag) {
            case "Key":
                saveItem = other.gameObject;
                ActivateItem(0);
                break;
            case "SparkPlug":
                saveItem = other.gameObject;
                ActivateItem(1);
                break;
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player"))
            playerOnElevator = false;
    }
}
