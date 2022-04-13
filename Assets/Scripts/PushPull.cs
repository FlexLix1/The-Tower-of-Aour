using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPull : MonoBehaviour {
    private float speed = 2.0f;
    public GameObject character;
    private Transform grabbedObj;

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1.25f);
            foreach (var collider in colliders) {
                if (collider.CompareTag("Box")) {
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
