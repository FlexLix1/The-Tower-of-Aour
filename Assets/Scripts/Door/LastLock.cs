using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLock:MonoBehaviour {

    public GameObject bigDoor;
    public Animator[] locks;
    Animator doorAnim;
    MeshRenderer meshRend;

    int unlocks;

    void Start() {
        meshRend = bigDoor.GetComponent<MeshRenderer>();
        locks[0].enabled = false;
        locks[1].enabled = false;
        doorAnim = GetComponent<Animator>();
    }

    public void Unlock() {
        if(unlocks == 2)
            return;

        if(unlocks == 0) {
            locks[0].enabled = true;
            Invoke(nameof(RemoveLock), 0.5f);
        }

        if(unlocks == 1) {
            locks[1].enabled = true;
            Invoke(nameof(RemoveLock), 0.5f);
        }

    }

    void RemoveLock() {
        Destroy(locks[unlocks].gameObject);
        unlocks++;
        if(unlocks == 2) {
            meshRend.materials[4].EnableKeyword("_EMISSION");
            meshRend.materials[1].EnableKeyword("_EMISSION");
            doorAnim.Play("door_open");
        }
    }
}
