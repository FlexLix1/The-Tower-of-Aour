using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform:MonoBehaviour {
    [SerializeField] float distanceToCover;
    [SerializeField] float speed;
    public bool timeFroze, flipMovement, rayIsHovering, leftRight;

    MeshRenderer meshRenderer;

    Vector3 startPosition, left, right, up, down;

    void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        startPosition  = transform.position;
        right = startPosition + transform.right * distanceToCover;
        left =  startPosition - transform.right * distanceToCover;
        up = startPosition + transform.up * distanceToCover;
        down =  startPosition - transform.up * distanceToCover;
    }

    void FixedUpdate() {
        if(rayIsHovering) {
            meshRenderer.material.EnableKeyword("_EMISSION");
        } else {
            meshRenderer.material.DisableKeyword("_EMISSION");
        }

        if(timeFroze)
            return;

        if (leftRight)
        {
            if (!flipMovement)
            {
                MoveTowards(right);
            }
            else
            {
                MoveTowards(left);
            }
        }
        else
        {
            if (!flipMovement)
                MoveTowards(up);
            else
                MoveTowards(down);
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
