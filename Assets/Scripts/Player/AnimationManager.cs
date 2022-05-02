using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager:MonoBehaviour {

    Pushing pushBoxScript;
    Rigidbody rgbd;
    Animator anim;
    float velocityMagnitude;
    public float blendSpeed;

    string currentState;

    void Start() {
        rgbd = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        pushBoxScript = GetComponent<Pushing>();
    }

    void Update() {
        if(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) {
            velocityMagnitude += Time.deltaTime * blendSpeed;
        } else {
            velocityMagnitude -= Time.deltaTime * blendSpeed;
        }
        velocityMagnitude = Mathf.Clamp(velocityMagnitude, 0, 1);

        if(pushBoxScript.hasBox) {
            ChangeAnimation("IdleToPush");
            return;
        }

        ChangeAnimation("IdleToWalk");
    }

    void ChangeAnimation(string newState) {
        anim.Play(newState);
        anim.SetFloat(newState, velocityMagnitude);
    }
}
