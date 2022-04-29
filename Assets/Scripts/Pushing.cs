using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushing : MonoBehaviour {
    public GameObject tempBox;
    public float rayDistance = 2f, boxOffset = 2f;
    public bool hasBox;
    Vector3 offset;
    public enum lockDirection {LeftUp, DownRight};
    public lockDirection holdDirection;

    void Update() {
        if (hasBox) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                hasBox = false;
                tempBox = null;
                return;
            }

            MovingBox();
            if (Input.GetKeyDown(KeyCode.E)) {
                hasBox = false;
                tempBox = null;
            }
            return;
        }

        if (!Input.GetKeyDown(KeyCode.E)) return;

        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.forward * rayDistance, out hit, rayDistance)) {
            if (hit.collider.CompareTag("Box")) {
                tempBox = hit.collider.gameObject;
                Vector3 forcedDir = tempBox.transform.position - transform.position;
                float angle = Mathf.Atan2(forcedDir.z, forcedDir.x) * Mathf.Rad2Deg;
                if (angle < 35 && angle > -35) {
                    //Vänster
                    offset = Vector3.right * boxOffset;
                    holdDirection = lockDirection.LeftUp;
                } else if (angle > 55 && angle < 125) {
                    //Ner
                    offset = Vector3.forward * boxOffset;
                    holdDirection = lockDirection.DownRight;
                } else if (angle < -55 && angle > -125) {
                    //Upp
                    offset = -Vector3.forward * boxOffset;
                    holdDirection = lockDirection.DownRight;
                } else if (angle > 125 || angle < -125) {
                    //Höger
                    offset = -Vector3.right * boxOffset;
                    holdDirection = lockDirection.LeftUp;
                }
                hasBox = true;
            }
        }
    }

    void MovingBox() {
        tempBox.transform.position = transform.position + offset;
    }
}
