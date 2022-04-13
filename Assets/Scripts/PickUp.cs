using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp:MonoBehaviour {

    public GameObject[] inventory, prefabsInventory;
    public float rayDistance = 1.5f, placeDistance = 2;
    GameObject savePickup;
    int saveInventoryNumber;
    bool hasPickup, canPickup = true;

    void Update() {
        if(Input.GetKeyDown(KeyCode.E) && hasPickup) {
            PlaceItem();
        }

        if(Input.GetKeyDown(KeyCode.E) && !hasPickup && canPickup) {
            CheckPickup();
        }
    }

    void PlaceItem() {
        //Add raycast too make sure the item can be placed
        Instantiate(prefabsInventory[saveInventoryNumber], transform.position + (transform.forward * placeDistance), Quaternion.identity);
        inventory[saveInventoryNumber].SetActive(false);
        hasPickup = false;
        Invoke(nameof(CanPickup), 0.025f);
    }

    void CheckPickup() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward * rayDistance, out hit, rayDistance)) {
            switch(hit.collider.tag) {
                case "Key":
                    savePickup = hit.collider.gameObject;
                    PickInventory(0);
                    break;
            }
        }
    }

    void PickInventory(int selectedNumber) {
        inventory[selectedNumber].SetActive(true);
        saveInventoryNumber = selectedNumber;
        hasPickup = true;
        canPickup = false;
        Destroy(savePickup);
    }

    void CanPickup() {
        canPickup = true;
    }
}
