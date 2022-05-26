using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityCore {
    namespace Audio {

        public class Interact:MonoBehaviour {

            Bird birdScript;
            AnimationManager animScript;
            PlayerMovement movementScript;
            DynamicLever leverScript;
            DynamicDoor doorScript;
            Generator generatorScript;
            PickUp inventoryScript;
            DynamicLastDoor lastDoor;
            string saveTag;
            bool onTrigger, moveTowards;

            Rigidbody rgbd;
            GameObject holdLever;

            void Start() {
                movementScript = GetComponent<PlayerMovement>();
                animScript = GetComponent<AnimationManager>();
                lastDoor = GetComponent<DynamicLastDoor>();
                inventoryScript = GetComponent<PickUp>();
                rgbd = GetComponent<Rigidbody>();
            }

            void Update() {
                if(moveTowards) {
                    MoveTowards(holdLever.transform.position + (-holdLever.transform.forward * 0.7f));
                    return;
                }

                if(!onTrigger)
                    return;

                if(Input.GetKeyDown(KeyCode.E)) {
                    UseItem();
                }
            }

            void UseItem() {
                switch(saveTag) {
                    case "Lever":
                        if(inventoryScript.hasPickup)
                            return;

                        if(leverScript.inUse)
                            return;

                        movementScript.moveTowardsLever = true;
                        moveTowards = true;
                        break;
                    case "Door":
                        if(!inventoryScript.inventory[0].activeInHierarchy)
                            return;

                        doorScript.openDoor = true;
                        inventoryScript.UseItem();
                        break;
                    case "Generator":
                        if(!inventoryScript.inventory[1].activeInHierarchy)
                            return;

                        generatorScript.generatorActive = true;
                        inventoryScript.UseItem();
                        break;
                    case "BirdFuck":
                        if(!inventoryScript.inventory[1].activeInHierarchy)
                            return;

                        birdScript.bird = true;
                        inventoryScript.UseItem();
                        break;

                    //case "LastDoor":
                    //    if (!inventoryScript.inventory[0].activeInHierarchy)
                    //        return;
                    //    inventoryScript.UseItem();

                    //    if(lastDoor.keysAdded == 2)
                    //    {
                    //        lastDoor.openDoor = true;

                    //    }
                    //    break;
                }
            }

            void MoveTowards(Vector3 destination) {
                Vector3 forcedDirection = destination - transform.position;
                if(Vector3.Distance(destination, transform.position) > 0.2f) {
                    rgbd.velocity = forcedDirection * 3;
                    return;
                }
                moveTowards = false;
                transform.forward = holdLever.transform.forward;
                leverScript.FlipSwitch();
            }

            public void LeverAnimComplete() {
                movementScript.moveTowardsLever = false;
                if(leverScript.leverActive) {
                    leverScript.SetLeverFalse();
                } else {
                    leverScript.SetLeverTrue();
                }
            }

            void OnTriggerEnter(Collider other) {
                switch(other.tag) {
                    case "Lever":
                        onTrigger = true;
                        saveTag = other.tag;
                        holdLever = other.gameObject;
                        leverScript = animScript.leverScript = holdLever.GetComponent<DynamicLever>();
                        break;
                    case "Door":
                        onTrigger = true;
                        saveTag = other.tag;
                        doorScript = other.gameObject.GetComponent<DynamicDoor>();
                        break;
                    case "Generator":
                        onTrigger = true;
                        saveTag = other.tag;
                        generatorScript = other.gameObject.GetComponent<Generator>();
                        break;
                    case "BirdFuck":
                        onTrigger = true;
                        saveTag = other.tag;
                        birdScript = other.gameObject.GetComponent<Bird>();
                        break;
                }
            }

            void OnTriggerExit(Collider other) {
                if(onTrigger) {
                    onTrigger = false;
                    saveTag = null;
                    leverScript = null;
                    doorScript = null;
                    generatorScript = null;
                    birdScript = null;
                }
            }
        }
    }
}
