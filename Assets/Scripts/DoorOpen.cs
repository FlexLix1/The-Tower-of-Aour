using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen:MonoBehaviour {

    Rigidbody rgbd;
    public bool doorOpen;

    void Start() {
        rgbd = GetComponent<Rigidbody>();
    }

    void Update() {
        if(doorOpen) {
            rgbd.constraints = ~RigidbodyConstraints.FreezeRotationY;
        }
    }
}
