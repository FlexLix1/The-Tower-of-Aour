using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushing : MonoBehaviour {
    public GameObject tempBox;
    public float rayDistance = 2f, boxOffset = 2f;
    bool hasBox;
    Vector3 offset;
    public string lockDirection;
    void Update() {
        if (hasBox) {
            MovingBox();
            if (Input.GetKeyDown(KeyCode.E)) {
                hasBox = false;
                tempBox = null;
            }
            return;
        }
        if (!Input.GetKeyDown(KeyCode.E)) return;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward * rayDistance, out hit, rayDistance)) {
            if (hit.collider.CompareTag("Box")) {
                tempBox = hit.collider.gameObject;
                Vector3 forcedDir = tempBox.transform.position - transform.position;
                float angle = Mathf.Atan2(forcedDir.z, forcedDir.x) * Mathf.Rad2Deg;
                if (angle < 35 && angle > -35) {
                    //Vänster
                    offset = Vector3.right * boxOffset;
                    lockDirection = "LeftUp";
                } else if (angle > 55 && angle < 125) {
                    //Ner
                    offset = Vector3.forward * boxOffset;
                    lockDirection = "DownRight";
                } else if (angle < -55 && angle > -125) {
                    //Upp
                    offset = -Vector3.forward * boxOffset;
                    lockDirection = "LeftUp";
                } else if (angle > 125 || angle < -125) {
                    //Höger
                    offset = -Vector3.right * boxOffset;
                    lockDirection = "DownRight";
                }
                hasBox = true;
            }
        }
    }

    void MovingBox() {
        tempBox.transform.position = transform.position + offset;
    }
}
