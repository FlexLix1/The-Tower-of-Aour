using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement:MonoBehaviour {

    Vector3 playerPosition, relativeForward, relativeRight;
    Rigidbody rgbd;

    public float movementSpeed, rotationSpeed;
    float cameraAngle;

    public GameObject mainCamera;
    Pushing pushScript;

    public bool GroundMovement;
    private SlipperyOil slipperyOilMovement;

    void Start() {
        rgbd = GetComponent<Rigidbody>();
        slipperyOilMovement = GetComponent<SlipperyOil>();
        pushScript = GetComponent<Pushing>();
        GroundMovement = true;
    }

    void Update() {

        if(pushScript.hasBox) {
            switch(pushScript.holdDirection) {
                case Pushing.lockDirection.LeftUp:
                    rgbd.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                    break;
                case Pushing.lockDirection.DownRight:
                    rgbd.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
                    break;
            }
        } else {
            rgbd.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if(GroundMovement) {
            //Update forward direction relative to camera
            UpdateCameraForward();

            //Update player input
            GetInput();

            //Update player Input
            UpdatePosition();

            if(pushScript.hasBox)
                return;

            //Rotate character towards walking direction
            RotateCharacter();

        } else {
            slipperyOilMovement.SlipperyOilMovement();
        }
    }

    void UpdateCameraForward() {
        relativeForward = mainCamera.transform.forward;
        relativeForward.y = 0;
        Vector3.Normalize(relativeForward);
        relativeRight = Quaternion.Euler(new Vector3(0, 90, 0)) * relativeForward;
    }

    void GetInput() {
        Vector3 forwardDirection = Input.GetAxisRaw("Vertical") * relativeForward;
        Vector3 rightDirection = Input.GetAxisRaw("Horizontal") * relativeRight;
        playerPosition = Vector3.Normalize(rightDirection + forwardDirection) * movementSpeed;
    }

    void UpdatePosition() {
        playerPosition.y = rgbd.velocity.y;
        rgbd.velocity = playerPosition;
    }

    void RotateCharacter() {
        if(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) {
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(playerPosition.x, transform.position.y - transform.position.y, playerPosition.z), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
