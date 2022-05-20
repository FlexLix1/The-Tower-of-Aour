using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform:MonoBehaviour {
    [SerializeField] float distanceToCover;
    [SerializeField] float speed;
    public bool timeFroze, flipMovement, rayIsHovering;

    MeshRenderer meshRenderer;

    Vector3 startPosition, left, right;

    void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        startPosition  = transform.position;
        right = startPosition + transform.right * distanceToCover;
        left =  startPosition - transform.right * distanceToCover;
    }

    void FixedUpdate() {
        if(rayIsHovering) {
            meshRenderer.material.EnableKeyword("_EMISSION");
        } else {
            meshRenderer.material.DisableKeyword("_EMISSION");
        }

        if(timeFroze)
            return;

        if(!flipMovement) {
            MoveTowards(right);
        } else {
            MoveTowards(left);
        }
    }

    void MoveTowards(Vector3 destination) {
        Vector3 forcedDirection = destination - transform.position;
        if(Vector3.Distance(destination, transform.position) > 0.2f) {
            transform.position += forcedDirection.normalized * speed;
            return;
        }

        flipMovement = !flipMovement;
    }
}
