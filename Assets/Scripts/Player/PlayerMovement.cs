using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityCore {
    namespace Audio {
        public class PlayerMovement:MonoBehaviour {

            public Vector3 playerPosition;
            Vector3 relativeForward, relativeRight, destination;
            Rigidbody rgbd;

            public float movementSpeed, rotationSpeed;
            float cameraAngle, startMovementSpeed;

            public GameObject mainCamera;
            AnimationManager animScript;
            Pushing pushScript;

            public bool groundMovement, moveTowardsLever;
            private SlipperyOil slipperyOilMovement;

            AudioController audioController;

            void Start() {
                audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
                rgbd = GetComponent<Rigidbody>();
                pushScript = GetComponent<Pushing>();
                slipperyOilMovement = GetComponent<SlipperyOil>();
                animScript = GetComponent<AnimationManager>();
                startMovementSpeed = movementSpeed;
                groundMovement = true;

            }

            void Update() {
                if(moveTowardsLever)
                    return;

                if(animScript.climbingBox) {
                    if(animScript.climbingBox) {
                        Vector3 forcedDirection = destination - transform.position;
                        Debug.Log(Vector3.Distance(destination, transform.position));
                        if(Vector3.Distance(destination, transform.position) > 0.005f) {
                            rgbd.velocity = forcedDirection.normalized * 2.5f;
                        } else {
                            rgbd.velocity = Vector3.zero;
                        }
                        return;
                    }
                }

                if(pushScript.movingPlayerTowardsBox)
                    return;

                if(pushScript.hasBox) {
                    movementSpeed = 3;
                    audioController.PlayAudio(AudioType.SFX_PullingBoxes, true);
                    switch(pushScript.holdLockDirection) {
                        case Pushing.lockDirection.LockAxisY:
                            rgbd.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                            break;
                        case Pushing.lockDirection.LockAxisX:
                            rgbd.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
                            break;
                    }
                } else {
                    movementSpeed = startMovementSpeed;
                    rgbd.constraints = RigidbodyConstraints.FreezeRotation;
                }

                if(!groundMovement)
                    return;

                //Update forward direction relative to camera
                UpdateCameraForward();

                if(Input.GetKey(KeyCode.LeftShift) && !pushScript.hasBox) {
                    movementSpeed = 10;
                } else {
                    movementSpeed = startMovementSpeed;
                }

                //Update player input
                GetInput();

                //Update player Input
                UpdatePosition();

                if(pushScript.hasBox)
                    return;

                //Rotate character towards walking direction
                RotateCharacter();

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
                destination = transform.position;
            }

            void RotateCharacter() {
                if(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) {
                    Quaternion toRotation = Quaternion.LookRotation(new Vector3(playerPosition.x, 0, playerPosition.z), Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
                }
            }

            public void BoxClimbFirstStep() {
                destination = new Vector3(transform.position.x, transform.position.y + 2.6f, transform.position.z);
            }

            public void BoxClimbSecondStep() {
                destination = transform.position + transform.forward;
            }

            void OnTriggerEnter(Collider other) {
                if(other.gameObject.name == "Platform") {
                    transform.SetParent(other.transform);
                }
            }

            void OnTriggerExit(Collider other) {
                if(other.gameObject.name == "Platform") {
                    transform.SetParent(null);
                }
            }

        }
    }
}
