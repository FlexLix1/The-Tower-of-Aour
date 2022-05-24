using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp:MonoBehaviour {

    public GameObject[] inventory, prefabsInventory;
    public float rayDistance = 1.5f, placeDistance = 2;
    public Transform itemInHand;
    GameObject savePickup;

    int saveInventoryNumber;
    public bool hasPickup;
    bool canPickup = true;

    void Update() {
        if(hasPickup) {
            inventory[saveInventoryNumber].transform.position = itemInHand.position;
        }

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
        if(Physics.Raycast(transform.position + transform.up, transform.forward * rayDistance, out hit, rayDistance))
            return;

        PlaceItem();
    }

    public void UseItem() {
        //Add keyopendoor soundFX
        inventory[saveInventoryNumber].SetActive(false);
        hasPickup = false;
        Invoke(nameof(CanPickup), 0.025f);
    }

    //Places held item on map
    void PlaceItem() {
        Instantiate(prefabsInventory[saveInventoryNumber], new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z) + (transform.forward * placeDistance), Quaternion.identity);
        inventory[saveInventoryNumber].SetActive(false);
        hasPickup = false;
        Invoke(nameof(CanPickup), 0.025f);
    }

    //Checks if item is pickupable
    void CheckPickup() {
        RaycastHit hit;
        if(Physics.Raycast(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), transform.forward * rayDistance, out hit, rayDistance)) {
            switch(hit.collider.tag) {
                case "Key":
                    savePickup = hit.collider.gameObject;
                    PickInventory(0);
                    break;
                case "SparkPlug":
                    savePickup = hit.collider.gameObject;
                    PickInventory(1);
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
