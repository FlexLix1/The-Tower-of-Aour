using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDoor:MonoBehaviour {
    [SerializeField] Vector3 doorTarget;
    Vector3 doorStart;

    public float speed;
    BoxCollider colliderActive;
    Rigidbody rgbd;

    public bool swiningDoor, openDoor;
    bool moving;

    void Start() {
        rgbd = GetComponent<Rigidbody>();
        colliderActive = GetComponent<BoxCollider>();
        doorStart = transform.position;
        doorTarget += doorStart;
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

        if(!openDoor) {
            if(Vector3.Distance(doorStart, transform.position) > 0.025f) {
                MoveDoorTo(doorStart);
            } else {
                rgbd.velocity = Vector3.zero;
                colliderActive.isTrigger = false;
            }
            return;
        }

        colliderActive.isTrigger = true;
        if(Vector3.Distance(doorTarget, transform.position) > 0.025f) {
            MoveDoorTo(doorTarget);
        } else {
            rgbd.velocity = Vector3.zero;
        }
    }

    void MoveDoorTo(Vector3 target) {
        Vector3 forcedDirection = target - transform.position;
        Vector3.Normalize(forcedDirection);
        rgbd.velocity = forcedDirection * speed;
    }
}
