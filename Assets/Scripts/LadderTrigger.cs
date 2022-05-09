using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTrigger : MonoBehaviour {
    public Ladder climb;
    public GameObject player;
    public float facingLadder;
    public BoxCollider upperCollider, lowerCollider;
    public bool isTop;
    public GameObject ladderPostion;

    void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;
        if (Vector3.Distance(lowerCollider.center, other.transform.position)
            < Vector3.Distance(upperCollider.center, other.transform.position)) {
            if (isTop) {
                climb.isClimbing = false;
                isTop = false;
            } else {
                climb.isClimbing = true;
                player.transform.rotation = Quaternion.Euler(0, facingLadder, 0);
                player.transform.position = ladderPostion.transform.position + ladderPostion.transform.forward;
            }
        }
        if (Vector3.Distance(upperCollider.center, other.transform.position)
            < Vector3.Distance(lowerCollider.center, other.transform.position)) {
            if (isTop) {
                climb.isClimbing = true;
                player.transform.rotation = Quaternion.Euler(0, facingLadder, 0);
                player.transform.position = ladderPostion.transform.position + ladderPostion.transform.forward;
            } else {
                climb.isClimbing = false;
                isTop = true;
            }
        }
    }
}