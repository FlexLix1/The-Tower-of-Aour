using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BouncingArrow:MonoBehaviour {

    Vector3 startPos;
    public float speed = 10, size = 4;

    void Start() {
        startPos = transform.position;
    }

    void Update() {
        transform.position = startPos + new Vector3(0, Mathf.Cos(Time.time * speed) * size, 0);
    }
}
