using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement:MonoBehaviour {

    Vector3 playerPosition;
    public float movementSpeed, rotationSpeed;
    Rigidbody rgbd;

    float direction, angle;

    public GameObject mainCamera;

    void Start() {
        rgbd = GetComponent<Rigidbody>();
    }

    void Update() {
        //Update player input
        GetInput();

        //RotateCharacter();
        RotateCharacter();

        //Get angle between player and camera (Exclude Y axis)
        CameraPlayerAngle();

        //Update player Input
        UpdatePosition();
    }

    void GetInput() {
        playerPosition.z = Input.GetAxisRaw("Vertical");
        playerPosition.x = Input.GetAxisRaw("Horizontal");
        playerPosition = playerPosition.normalized * movementSpeed;
    }

    void UpdatePosition() {
        playerPosition.y = rgbd.velocity.y;
        rgbd.velocity = playerPosition;
    }

    void CameraPlayerAngle() {
        Vector3 getAngle = rgbd.transform.position - mainCamera.transform.position;
        angle = (Mathf.Atan2(getAngle.x, getAngle.z) * Mathf.Rad2Deg);
    }

    void RotateCharacter() {

    }
}
