using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDoor:MonoBehaviour {

    Rigidbody rgbd;

    public float speed = 3;

    public Vector3 moveTowards;
    public bool swiningDoor, openDoor;
    bool moving;

    void Start() {
        rgbd = GetComponent<Rigidbody>();
    }

    void Update() {

    }

    void MoveDoorTo(Vector3 target) {
        Vector3 forcedDirection = target - transform.position;
        Vector3.Normalize(forcedDirection);
        rgbd.velocity = forcedDirection * speed;
    }
}
