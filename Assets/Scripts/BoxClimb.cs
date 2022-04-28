using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxClimb:MonoBehaviour {

    public float rayDistance;

    void Update() {
        if(!Input.GetKeyDown(KeyCode.Space))
            return;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward * rayDistance, out hit, rayDistance)) {
            switch(hit.collider.tag) {
                case "Box":
                    GameObject holdBox = hit.collider.gameObject;
                    transform.position =  new Vector3(holdBox.transform.position.x, holdBox.transform.position.y + 4, holdBox.transform.position.z);
                    holdBox = null;
                    break;
            }
        }
    }
}
