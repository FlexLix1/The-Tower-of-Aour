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
            Lock lockScript;
            LastLock lastLockScript;
            Generator generatorScript;
            PickUp inventoryScript;
            string saveTag;
            bool onTrigger, moveTowards;

            Rigidbody rgbd;
            GameObject holdLever;

            void Start() {
                movementScript = GetComponent<PlayerMovement>();
                animScript = GetComponent<AnimationManager>();
                inventoryScript = GetComponent<PickUp>();
                rgbd = GetComponent<Rigidbody>();
            }

            void Update() {
                if(!onTrigger)
                    return;

                if(moveTowards) {
                    transform.forward = holdLever.transform.forward;
                    MoveTowards(holdLever.transform.position + (-holdLever.transform.forward * 0.7f));
                    return;
                }

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
                    case "Lock":
                        if(!inventoryScript.inventory[0].activeInHierarchy)
                            return;

                        lockScript.Unlock();
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
                    case "LastDoor":
                        if(!inventoryScript.inventory[0].activeInHierarchy)
                            return;

                        lastLockScript.Unlock();
                        inventoryScript.UseItem();
                        break;
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
                if(onTrigger)
                    return;

                switch(other.tag) {
                    case "Lever":
                        saveTag = other.tag;
                        holdLever = other.gameObject;
                        leverScript = animScript.leverScript = holdLever.GetComponent<DynamicLever>();
                        onTrigger = true;
                        break;
                    case "Lock":
                        saveTag = other.tag;
                        lockScript = other.gameObject.GetComponent<Lock>();
                        onTrigger = true;
                        break;
                    case "Generator":
                        saveTag = other.tag;
                        generatorScript = other.gameObject.GetComponent<Generator>();
                        onTrigger = true;
                        break;
                    case "BirdFuck":
                        saveTag = other.tag;
                        birdScript = other.gameObject.GetComponent<Bird>();
                        onTrigger = true;
                        break;
                    case "LastDoor":
                        saveTag = other.tag;
                        lastLockScript = other.gameObject.GetComponent<LastLock>();
                        onTrigger = true;
                        break;
                }
            }

            void OnTriggerExit(Collider other) {
                if(!onTrigger)
                    return;

                switch(other.tag) {
                    case "Lever":
                        saveTag = null;
                        holdLever = null;
                        leverScript = animScript.leverScript = null;
                        onTrigger = false;
                        break;
                    case "Lock":
                        saveTag = null;
                        lockScript = null;
                        onTrigger = false;
                        break;
                    case "Generator":
                        saveTag = null;
                        generatorScript = null;
                        onTrigger = false;
                        break;
                    case "BirdFuck":
                        saveTag = null;
                        birdScript = null;
                        onTrigger = false;
                        break;
                    case "LastDoor":
                        saveTag = null;
                        lastLockScript = null;
                        onTrigger = false;
                        break;
                }
            }
        }
    }
}
