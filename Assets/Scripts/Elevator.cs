using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator:MonoBehaviour {
    [SerializeField] Vector3 elevatorTarget;
    Vector3 elevatorStart;

    public float speed;
    Rigidbody rgbd;

    public bool elevatorActive;

    void Start() {
        rgbd = GetComponent<Rigidbody>();
        elevatorStart = transform.position;
        elevatorTarget += elevatorStart;
    }
    
    void Update() {

        if(!elevatorActive) {
            if(Vector3.Distance(elevatorStart, transform.position) > 0.05f) {
                MoveDoorTo(elevatorStart);
            } else {
                rgbd.velocity = Vector3.zero;
            }
            return;
        }

        if(Vector3.Distance(elevatorTarget, transform.position) > 0.05f) {
            MoveDoorTo(elevatorTarget);
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
