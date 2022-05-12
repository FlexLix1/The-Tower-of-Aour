using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager:MonoBehaviour {

    public DynamicLever leverScript;
    Ladder ladderClimbScript;
    Pushing pushBoxScript;
    Rigidbody rgbd;
    Animator anim;
    public float blendSpeed, velocityMagnitude;
    float runFloat, ladderFloat;

    string currentState;

    public bool climbingBox, usingLever;
    bool pushBox, pullBox;

    void Start() {
        rgbd = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        pushBoxScript = GetComponent<Pushing>();
        ladderClimbScript = GetComponent<Ladder>();
    }

    void Update() {
        if(ladderClimbScript.isClimbing) {
            LadderClimb();
            return;
        }

        if(usingLever) {
            if(leverScript.leverActive) {
                anim.Play("CharLeverUp");
            } else {
                anim.Play("CharLeverDown");
            }
            return;
        }

        if(climbingBox) {
            anim.Play("BoxClimb");
            velocityMagnitude = 0;
            return;
        }

        if(rgbd.velocity.magnitude > 0.2) {
            velocityMagnitude += Time.deltaTime * blendSpeed;
        } else {
            velocityMagnitude -= Time.deltaTime * (blendSpeed * 0.5f);
        }
        velocityMagnitude = Mathf.Clamp(velocityMagnitude, 0, 1);

        if(pushBoxScript.hasBox) {
            CheckPushDirection();
            return;
        }

        if(Input.GetKey(KeyCode.LeftShift) && rgbd.velocity.magnitude > 0.2) {
            runFloat += Time.deltaTime * blendSpeed;
        } else {
            runFloat -= Time.deltaTime * blendSpeed;
        }
        runFloat = Mathf.Clamp(runFloat, 0, 1);

        anim.SetFloat("IdleToWalk", velocityMagnitude);
        anim.SetFloat("WalkToRun", runFloat);
        anim.Play("IdleToWalkToRun");
    }

    public void ChangeAnimation(string newState) {
        anim.Play(newState);
        anim.SetFloat(newState, velocityMagnitude);
    }

    void LadderClimb() {
        if(rgbd.velocity.magnitude > 0.2f) {
            anim.enabled = true;
        } else {
            anim.enabled = false;
        }

        if(Input.GetAxisRaw("Vertical") > 0) {
            ladderFloat += Time.deltaTime * blendSpeed;
        } else if(Input.GetAxisRaw("Vertical") < 0) {
            ladderFloat -= Time.deltaTime * blendSpeed;
        }
        ladderFloat = Mathf.Clamp(ladderFloat, -1, 1);

        anim.Play("LadderClimb");
        anim.SetFloat("LadderBlend", ladderFloat);
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
