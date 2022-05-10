using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager:MonoBehaviour {

    Pushing pushBoxScript;
    Rigidbody rgbd;
    Animator anim;
    public float blendSpeed, velocityMagnitude;

    string currentState;

    public bool climbingBox;
    bool pushBox, pullBox;

    void Start() {
        rgbd = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        pushBoxScript = GetComponent<Pushing>();
    }

    void Update() {
        if(climbingBox) {
            anim.Play("BoxClimb");
            velocityMagnitude = 0;
            return;
        }

        if(rgbd.velocity.magnitude > 0.2) {
            velocityMagnitude += Time.deltaTime * blendSpeed;
        } else {
            velocityMagnitude -= Time.deltaTime * blendSpeed;
        }
        velocityMagnitude = Mathf.Clamp(velocityMagnitude, 0, 1);

        if(pushBoxScript.hasBox) {
            CheckPushDirection();
            return;
        }

        if(Input.GetKey(KeyCode.LeftShift)) {
            anim.SetFloat("IdleToRun", velocityMagnitude);
            return;
        }
        ChangeAnimation("IdleToWalk");
    }

    public void ChangeAnimation(string newState) {
        anim.Play(newState);
        anim.SetFloat(newState, velocityMagnitude);
    }

    void CheckPushDirection() {
        switch(pushBoxScript.holdPushDirection) {
            case Pushing.pushDirection.Left:
                if(rgbd.velocity.x > 0) {
                    pullBox = false;
                    pushBox = true;
                } else if(rgbd.velocity.x < 0) {
                    pushBox = false;
                    pullBox = true;
                }
                break;
            case Pushing.pushDirection.Down:
                if(rgbd.velocity.z > 0) {
                    pullBox = false;
                    pushBox = true;
                } else if(rgbd.velocity.z < 0) {
                    pushBox = false;
                    pullBox = true;
                }
                break;
            case Pushing.pushDirection.Up:
                if(rgbd.velocity.z > 0) {
                    pushBox = false;
                    pullBox = true;
                } else if(rgbd.velocity.z < 0) {
                    pullBox = false;
                    pushBox = true;
                }
                break;
            case Pushing.pushDirection.Right:
                if(rgbd.velocity.x > 0) {
                    pushBox = false;
                    pullBox = true;
                } else if(rgbd.velocity.x < 0) {
                    pullBox = false;
                    pushBox = true;
                }
                break;
        }

        if(pushBox) {
            ChangeAnimation("IdleToPush");
        } else if(pullBox) {
            ChangeAnimation("IdleToPull");
        }
    }
}
