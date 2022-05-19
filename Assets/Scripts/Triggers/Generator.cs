using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator:MonoBehaviour {

    Animator anim;
    public GameObject sparkPlug, bigHeavyDoor;
    GameObject plugInstance;
    DynamicDoor doorScript;
    MeshRenderer meshRend;

    Vector3 destination;

    public bool generatorActive;

    void Start() {
        doorScript = bigHeavyDoor.GetComponent<DynamicDoor>();
        if(bigHeavyDoor != null) {
            meshRend = bigHeavyDoor.GetComponent<MeshRenderer>();
        }

        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    void Update() {
        if(!generatorActive)
            return;

        if(bigHeavyDoor != null) {
            meshRend.materials[4].EnableKeyword("_EMISSION");
            meshRend.materials[1].EnableKeyword("_EMISSION");
            anim.enabled = true;
            doorScript.openDoor = true;
            if(plugInstance == null) {
                plugInstance = Instantiate(sparkPlug, transform.position + transform.up * 2.575f, Quaternion.Euler(-90, 0, 0), transform);
            }
        }
    }
}
