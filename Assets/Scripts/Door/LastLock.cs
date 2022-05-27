using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLock:MonoBehaviour {

    public Animator[] locks;
    Animator doorAnim;

    int unlocks;

    void Start() {
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
            doorAnim.Play("door_open");
        }
    }
}
