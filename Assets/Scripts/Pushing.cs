using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushing:MonoBehaviour {
    GameObject tempBox;
    public float rayDistance = 2f, boxOffset = 2f;
    public bool hasBox;
    Vector3 offset;
    public enum lockDirection { LeftUp, DownRight };
    public lockDirection holdLockDirection;

    public enum pushDirection { Up, Down, Left, Right };
    public pushDirection holdPushDirection;


    void Update() {
        if(hasBox) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                hasBox = false;
                tempBox = null;
                return;
            }

            MovingBox();
            if(Input.GetKeyDown(KeyCode.E)) {
                hasBox = false;
                tempBox = null;
            }
            return;
        }

        if(!Input.GetKeyDown(KeyCode.E))
            return;

        RaycastHit hit;
        if(Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.forward * rayDistance, out hit, rayDistance)) {
            if(hit.collider.CompareTag("Box")) {
                tempBox = hit.collider.gameObject;
                Vector3 forcedDir = tempBox.transform.position - transform.position;
                float angle = Mathf.Atan2(forcedDir.z, forcedDir.x) * Mathf.Rad2Deg;
                if(angle < 35 && angle > -35) {
                    //Left
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    offset = Vector3.right * boxOffset;
                    holdLockDirection = lockDirection.LeftUp;
                    holdPushDirection = pushDirection.Left;
                } else if(angle > 55 && angle < 125) {
                    //Down
                    transform.rotation = Quaternion.Euler(Vector3.zero);
                    offset = Vector3.forward * boxOffset;
                    holdLockDirection = lockDirection.DownRight;
                    holdPushDirection = pushDirection.Down;
                } else if(angle < -55 && angle > -125) {
                    //Up
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    offset = -Vector3.forward * boxOffset;
                    holdLockDirection = lockDirection.DownRight;
                    holdPushDirection = pushDirection.Up;
                } else if(angle > 125 || angle < -125) {
                    //Right
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                    offset = -Vector3.right * boxOffset;
                    holdLockDirection = lockDirection.LeftUp;
                    holdPushDirection = pushDirection.Right;
                }
                hasBox = true;
            }
        }
    }

    void MovingBox() {
        tempBox.transform.position = transform.position + offset;
    }
}
