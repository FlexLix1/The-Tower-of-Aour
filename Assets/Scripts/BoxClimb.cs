using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxClimb:MonoBehaviour {

    GameObject holdBox;
    AnimationManager animManager;
    public float rayDistance;

    void Start() {
        animManager = GetComponent<AnimationManager>();
    }

    void Update() {
        if(animManager.climbingBox)
            return;

        if(!Input.GetKeyDown(KeyCode.Space))
            return;

        RaycastHit hit;
        if(Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.forward * rayDistance, out hit, rayDistance)) {
            switch(hit.collider.tag) {
                case "Box":
                    holdBox = hit.collider.gameObject;
                    Vector3 forcedDir = holdBox.transform.position - transform.position;
                    float angle = Mathf.Atan2(forcedDir.z, forcedDir.x) * Mathf.Rad2Deg;
                    if(angle < 35 && angle > -35) {
                        //Left
                        transform.rotation = Quaternion.Euler(0, 90, 0);
                    } else if(angle > 55 && angle < 125) {
                        //Down
                        transform.rotation = Quaternion.Euler(Vector3.zero);
                    } else if(angle < -55 && angle > -125) {
                        //Up
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    } else if(angle > 125 || angle < -125) {
                        //Right
                        transform.rotation = Quaternion.Euler(0, -90, 0);
                    }

                    animManager.climbingBox = true;
                    Invoke(nameof(WaitForAnimation), 4.9f);
                    break;
            }
        }
    }

    void WaitForAnimation() {
        transform.position = new Vector3(holdBox.transform.position.x, holdBox.transform.position.y + 2.5f, holdBox.transform.position.z);
        holdBox = null;
        animManager.climbingBox = false;
        CancelInvoke();
    }
}
