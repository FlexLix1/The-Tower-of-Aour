using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTrigger : MonoBehaviour {
    public Ladder climb;
    public GameObject player;
    public float facingLadder;
    public BoxCollider upperCollider, lowerCollider;
    public bool isTop, isBottom;
    public GameObject ladderPostion;

    void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;
        if (Vector3.Distance(lowerCollider.center, other.transform.position)
            < Vector3.Distance(upperCollider.center, other.transform.position)) {
            if (!isBottom) {
                isTop = false;
                isBottom = true;
                climb.isClimbing = true;
                player.transform.rotation = Quaternion.Euler(0, facingLadder, 0);
                Vector3 playerOffset = transform.position + transform.forward;
                player.transform.position = new Vector3(playerOffset.x, player.transform.position.y, playerOffset.z);
            } else {
                isBottom = false;
                climb.isClimbing = false;
            }

            if (isTop) {
                climb.isClimbing = false;
                isTop = false;
            } else {
                climb.isClimbing = true;
                player.transform.rotation = Quaternion.Euler(0, facingLadder, 0);
                Vector3 playerOffset = transform.position + transform.forward;
                Debug.Log(playerOffset);
                player.transform.position = new Vector3(playerOffset.x, player.transform.position.y, playerOffset.z);
            }
        }
        if (Vector3.Distance(upperCollider.center, other.transform.position)
            < Vector3.Distance(lowerCollider.center, other.transform.position)) {
            if (!isTop) {
                isBottom = false;
                isTop = true;
                climb.isClimbing = true;
                player.transform.rotation = Quaternion.Euler(0, facingLadder, 0);
                Vector3 playerOffset = transform.position + transform.forward;
                player.transform.position = new Vector3(playerOffset.x, player.transform.position.y, playerOffset.z);
            } else {
                isTop = false;
                climb.isClimbing = false;
            }
        }
    }
}