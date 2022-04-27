using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPull : MonoBehaviour {
    public GameObject character;
    private Transform grabbedObj;
    private Rigidbody rb;

    private void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update() {

        if (Input.GetKey(KeyCode.E)) {
            Collider[] colliders = Physics.OverlapBox(transform.position, transform.forward, Quaternion.identity);
            foreach (var collider in colliders) {
                if (collider.CompareTag("Box")) {
                    rb.isKinematic = true;
                    grabbedObj = collider.transform;
                    grabbedObj.SetParent(transform);
                    break;
                }
            }
        } else if (Input.GetKey(KeyCode.Q)) {
            if (grabbedObj != null) {
                grabbedObj.SetParent(null);
                grabbedObj = null;
                rb.isKinematic = false;
            }
        }

    }
}
