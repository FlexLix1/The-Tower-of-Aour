using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement:MonoBehaviour {

    Vector3 playerPosition, direction;
    Rigidbody rgbd;

    public float movementSpeed, rotationSpeed;
    float cameraAngle;

    public GameObject mainCamera;

    void Start() {
        rgbd = GetComponent<Rigidbody>();
    }

    void Update() {
        //Update player input
        GetInput();

        //Rotate character towards walking direction
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
        cameraAngle = (Mathf.Atan2(getAngle.x, getAngle.z) * Mathf.Rad2Deg);
    }

    void RotateCharacter() {
        if(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) {
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(playerPosition.x, transform.position.y - transform.position.y, playerPosition.z), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
