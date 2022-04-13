using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp:MonoBehaviour {

    public GameObject[] inventory, prefabsInventory;
    public float rayDistance = 1.5f, placeDistance = 2;
    GameObject savePickup;
    int saveInventoryNumber;
    bool hasPickup = false;

    void Update() {

        if(Input.GetKeyDown(KeyCode.E) && hasPickup) {
            PlacePickup();
        }

        if(Input.GetKeyDown(KeyCode.E) && !hasPickup) {
            CheckPickup();
        }
    }

    void PlacePickup() {
        //Add raycast too make sure the item can be placed
        Instantiate(savePickup, transform.position + (transform.forward * placeDistance), Quaternion.identity);
        inventory[saveInventoryNumber].SetActive(false);
        hasPickup = false;
    }

    void CheckPickup() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward * rayDistance, out hit, rayDistance)) {
            if(hit.collider.tag == "Pickup") {
                for(int i = 0; i < inventory.Length; i++) {
                    if(hit.collider.name == inventory[i].name) {
                        inventory[i].gameObject.SetActive(true);
                        savePickup = inventory[i];
                        saveInventoryNumber = i;
                        hasPickup = true;
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
    }
}
