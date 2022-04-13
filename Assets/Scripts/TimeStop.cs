using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop:MonoBehaviour {

    public Material timeFreezeMat, normalMat;
    MeshRenderer material;
    Rigidbody rgbd;

    Vector3 saveVelocity;

    public float freezeTime = 5;
    public bool timeStoped, isRotating;
    float time;

    void Start() {
        material = GetComponent<MeshRenderer>();
        rgbd = GetComponent<Rigidbody>();
    }

    void Update() {

        if(Input.GetMouseButtonDown(1)) {
            timeStoped = !timeStoped;
            Unfreeze();
        }

        if(!timeStoped)
            return;

        if(time < freezeTime) {
            time += Time.deltaTime;
            rgbd.velocity = Vector3.zero;
            return;
        }
        Unfreeze();
    }

    void Unfreeze() {
        time = 0;
        material.material = normalMat;
        rgbd.constraints = RigidbodyConstraints.None;
        if(isRotating) {
            rgbd.constraints = RigidbodyConstraints.FreezePosition;
            rgbd.constraints = RigidbodyConstraints.FreezeRotationZ;
            rgbd.constraints = RigidbodyConstraints.FreezeRotationX;
        }

        rgbd.velocity = saveVelocity;
        timeStoped = false;
    }

    void OnMouseOver() {
        if(timeStoped)
            return;

        if(Input.GetMouseButtonDown(0)) {
            timeStoped = true;
            saveVelocity = rgbd.velocity;
            material.material = timeFreezeMat;
            rgbd.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
