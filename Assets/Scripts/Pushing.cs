using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityCore {
    namespace Audio {

        public class Pushing : MonoBehaviour {
            AudioController audioController;
            GameObject holdBox;
            public float rayDistance = 2f, boxOffset = 2f;
            public bool hasBox, movingPlayerTowardsBox;

            Vector3 offset;
            public enum lockDirection { LockAxisY, LockAxisX };
            public lockDirection holdLockDirection;

            public enum pushDirection { Up, Down, Left, Right };
            public pushDirection holdPushDirection;

            Quaternion holdRotation;

            Rigidbody rgbd;
            Box boxScript;

            void Start() {
                rgbd = GetComponent<Rigidbody>();
            }

            void Update() {
                if (movingPlayerTowardsBox) {
                    PlayerTowardsBox();
                    return;
                }

                if (hasBox) {
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        hasBox = false;
                        boxScript.hasBox = false;
                        holdBox = null;
                        return;
                    }

                    if (Input.GetKeyDown(KeyCode.E)) {
                        hasBox = false;
                        boxScript.hasBox = false;
                        holdBox = null;
                    }
                    return;
                }

                if (!Input.GetKeyDown(KeyCode.E))
                    return;



                RaycastHit hit;
                if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.forward * rayDistance, out hit, rayDistance)) {
                    if (hit.collider.CompareTag("Box")) {
                        holdBox = hit.collider.gameObject;
                        RaycastHit aboveHit;
                        if (Physics.Raycast(new Vector3(holdBox.transform.position.x, holdBox.transform.position.y + 2, holdBox.transform.position.z), transform.up, out aboveHit, 1))
                            return;

                        boxScript = holdBox.GetComponent<Box>();
                        Vector3 forcedDir = holdBox.transform.position - transform.position;
                        float angle = Mathf.Atan2(forcedDir.z, forcedDir.x) * Mathf.Rad2Deg;
                        if (angle < 35 && angle > -35) {
                            //Left
                            holdRotation = Quaternion.Euler(0, 90, 0);
                            boxScript.offset = offset = Vector3.right * boxOffset;
                            holdLockDirection = lockDirection.LockAxisY;
                            holdPushDirection = pushDirection.Left;
                        } else if (angle > 55 && angle < 125) {
                            //Down
                            holdRotation = Quaternion.Euler(Vector3.zero);
                            boxScript.offset = offset = Vector3.forward * boxOffset;
                            holdLockDirection = lockDirection.LockAxisX;
                            holdPushDirection = pushDirection.Down;
                        } else if (angle < -55 && angle > -125) {
                            //Up
                            holdRotation = Quaternion.Euler(0, 180, 0);
                            boxScript.offset = offset = -Vector3.forward * boxOffset;
                            holdLockDirection = lockDirection.LockAxisX;
                            holdPushDirection = pushDirection.Up;
                        } else if (angle > 125 || angle < -125) {
                            //Right
                            holdRotation = Quaternion.Euler(0, -90, 0);
                            boxScript.offset = offset = -Vector3.right * boxOffset;
                            holdLockDirection = lockDirection.LockAxisY;
                            holdPushDirection = pushDirection.Right;
                        }
                        movingPlayerTowardsBox = true;
                    }
                }
            }

            void PlayerTowardsBox() {
                Vector3 forcedDirection = (holdBox.transform.position - offset) - transform.position;
                forcedDirection = forcedDirection.normalized;
                if (Vector3.Distance(transform.position, (holdBox.transform.position - offset)) > 0.2f) {
                    rgbd.velocity = forcedDirection * 3;
                    transform.LookAt(holdBox.transform);
                } else {
                    rgbd.velocity = Vector3.zero;
                    movingPlayerTowardsBox = false;
                    Invoke(nameof(HasBox), 0.5f);
                }
            }

            void HasBox() {
                boxScript.hasBox = true;
                hasBox = true;
                transform.rotation = holdRotation;
                CancelInvoke();
            }
        }
    }
}
