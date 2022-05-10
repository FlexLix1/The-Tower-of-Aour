using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box:MonoBehaviour {

    PlayerMovement movementScript;
    Rigidbody rgbd, playerRGBD;

    GameObject player;
    public bool hasBox, checkAbove;
    public Vector3 offset;

    void Start() {
        rgbd = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRGBD = player.GetComponent<Rigidbody>();
    }

    void Update() {
        if(!hasBox) {
            rgbd.constraints = RigidbodyConstraints.FreezeRotation | ~RigidbodyConstraints.FreezePositionY;
            return;
        }

        rgbd.constraints = RigidbodyConstraints.FreezeRotation;
        rgbd.velocity = playerRGBD.velocity;
    }
}
