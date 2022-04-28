using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager:MonoBehaviour {

    Rigidbody rgbd;
    Animator anim;
    float velocityMagnitude;
    public float blendSpeed;

    void Start() {
        rgbd = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        if(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) {
            velocityMagnitude += Time.deltaTime * blendSpeed;
        } else {
            
            velocityMagnitude -= Time.deltaTime * blendSpeed;
        }
        velocityMagnitude = Mathf.Clamp(velocityMagnitude, 0, 1);

        anim.SetFloat("IdleToWalk", velocityMagnitude);
    }
}
