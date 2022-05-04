using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnyScript:MonoBehaviour {

    Rigidbody rgbd;
    public float speed;

    void Start() {
        rgbd = GetComponent<Rigidbody>();
    }

    void Update() {
        rgbd.angularVelocity = new Vector3(0, speed, 0);
    }
}
