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
            CheckPlacement();
        }

        if(Input.GetKeyDown(KeyCode.E) && !hasPickup && canPickup) {
            CheckPickup();
        }
    }

    //Check if there are obsticles
    void CheckPlacement() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward * rayDistance, out hit, rayDistance)) {
            switch(hit.collider.tag) {
                case "Door":
                    if(inventory[saveInventoryNumber].name == "Key") {
                        DoorOpen doorScript = hit.collider.gameObject.GetComponent<DoorOpen>();
                        doorScript.doorOpen = true;
                        UseItem();
                    }
                    return;
            }
            return;
        }
        PlaceItem();
    }

    //SUSS ligma baka omega sus, käre här talman, du e sussy baka; bin chillin 591536
    void UseItem() {
        inventory[saveInventoryNumber].SetActive(false);
        hasPickup = false;
        Invoke(nameof(CanPickup), 0.025f);
    }

    //Places held item on map
    void PlaceItem() {
        Instantiate(prefabsInventory[saveInventoryNumber], transform.position + (transform.forward * placeDistance), Quaternion.identity);
        inventory[saveInventoryNumber].SetActive(false);
        hasPickup = false;
        Invoke(nameof(CanPickup), 0.025f);
    }

    //Checks if item is pickupable
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

    //Activates, stores and destroys item in inventory and map
    void PickInventory(int selectedNumber) {
        inventory[selectedNumber].SetActive(true);
        saveInventoryNumber = selectedNumber;
        hasPickup = true;
        canPickup = false;
        Destroy(savePickup);
    }

    //Dumbfuck bool needs a cooldown
    void CanPickup() {
        canPickup = true;
    }
}
