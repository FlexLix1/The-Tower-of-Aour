using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTrigger : MonoBehaviour {
    public Ladder climb;
    public Transform target;



    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            climb.isClimbing = true;
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.transform.rotation = Quaternion.LookRotation(transform.forward);
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            climb.isClimbing = false;
        }
    }
}