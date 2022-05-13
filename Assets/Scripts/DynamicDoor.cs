using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDoor:MonoBehaviour {

    Rigidbody rgbd;

    public bool swiningDoor, openDoor;
    bool holdDoorState;

    public Animator anim;

    void Start() {
        rgbd = GetComponent<Rigidbody>();
        holdDoorState = openDoor;
        if(TryGetComponent<Animator>(out Animator animator)) {
            anim = animator;
        }
    }

    void Update() {

        if(swiningDoor) {
            if(openDoor) {
                rgbd.constraints = RigidbodyConstraints.None;
            } else {
                rgbd.constraints = RigidbodyConstraints.FreezeRotationY;
            }
            return;
        }

        if(holdDoorState == openDoor)
            return;

        if(openDoor) {
            DoorOpen();
        } else {
            DoorClose();
        }
    }

    void DoorOpen() {
        anim.Play("door_open");
    }

    void DoorClose() {
        anim.Play("door_close");
    }

    public void SetDoorOpen() {
        anim.Play("door_open_static");
        holdDoorState = openDoor;

    }

    public void SetDoorClosed() {
        anim.Play("door_close_static");
        holdDoorState = openDoor;
    }
}
