using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinscript:MonoBehaviour {

    public bool rayIsHovering, timeFroze;
    public float speedX, speedY, speedZ;

    MeshRenderer meshRenderer;

    void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.DisableKeyword("_EMISSION");
    }

    void Update() {

        if(rayIsHovering) {
            meshRenderer.material.EnableKeyword("_EMISSION");
        } else {
            meshRenderer.material.DisableKeyword("_EMISSION");
        }

        if(timeFroze)
            return;



        transform.Rotate(speedX * Time.deltaTime, speedY * Time.deltaTime, speedZ * Time.deltaTime, Space.World);
    }
}
