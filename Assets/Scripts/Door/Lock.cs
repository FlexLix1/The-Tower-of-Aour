using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock:MonoBehaviour {

    public DynamicDoor doorScript;
    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    public void Unlock() {
        anim.enabled = true;
        Invoke(nameof(RemoveLock), 0.5f);
    }

    void RemoveLock() {
        doorScript.openDoor = true;
        Destroy(gameObject);
    }
}
