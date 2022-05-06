using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box:MonoBehaviour {

    GameObject player;
    public bool hasBox;
    public Vector3 offset;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        if(!hasBox)
            return;

        transform.position = player.transform.position + offset;
    }
}
