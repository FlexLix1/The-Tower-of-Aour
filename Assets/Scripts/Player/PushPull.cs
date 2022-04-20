using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPull : MonoBehaviour {
    public GameObject character;
    private Transform grabbedObj;

    void Update() {
        if(Input.GetKeyDown(KeyCode.E)) {
            Collider[] colliders = Physics.OverlapBox(transform.position, transform.forward, Quaternion.identity);
            foreach(var collider in colliders) {
                if(collider.CompareTag("Box")) {
                    grabbedObj = collider.transform;
                    grabbedObj.SetParent(transform);
                    break;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.E)) {
            if (grabbedObj != null) {
                grabbedObj.SetParent(null);
                grabbedObj = null;
            }
        }

    }
}
