using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTrigger:MonoBehaviour {
    public Ladder climb;
    public GameObject player;
    public float facingLadder;
    public BoxCollider upperCollider, lowerCollider;
    public bool isTop, isBottom;
    public GameObject ladderPostion;

    void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player"))
            return;

        if(Vector3.Distance(lowerCollider.center, other.transform.position)
            < Vector3.Distance(upperCollider.center, other.transform.position)) {
            if(isBottom && !isTop) {
                climb.isClimbing = false;
                isBottom = false;
            } else if(!isBottom && isTop) {
                climb.isClimbing = false;
                isTop = false;
                isBottom = false;
            } else if(!isBottom && !isTop) {
                climb.isClimbing = true;
                player.transform.rotation = Quaternion.Euler(0, facingLadder, 0);
                Vector3 playerOffset = transform.position + transform.forward;
                player.transform.position = new Vector3(playerOffset.x, player.transform.position.y, playerOffset.z);
                isTop = false;
                isBottom = true;
            }
        }

        if(Vector3.Distance(upperCollider.center, other.transform.position)
            < Vector3.Distance(lowerCollider.center, other.transform.position)) {
            if(isTop && !isBottom) {
                climb.isClimbing = false;
                isTop = false;
            } else if(!isTop && isBottom) {
                climb.isClimbing = false;
                isTop = false;
                isBottom = false;
            } else if(!isTop && !isBottom) {
                climb.isClimbing = true;
                player.transform.rotation = Quaternion.Euler(0, facingLadder, 0);
                Vector3 playerOffset = transform.position + transform.forward;
                player.transform.position = new Vector3(playerOffset.x, player.transform.position.y, playerOffset.z);
                isTop = true;
                isBottom = false;
            }
        }
    }
}