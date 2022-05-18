using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore {
    namespace Audio {
        public class AnimationManager:MonoBehaviour {

            public DynamicLever leverScript;
            Ladder ladderClimbScript;
            Pushing pushBoxScript;
            Rigidbody rgbd;
            Animator anim;
            public float blendSpeed, velocityMagnitude;
            float runFloat, ladderFloat, boxPushPullFloat;
            AudioController audioController;

            string currentState;

            public bool climbingBox, usingLever;
            bool pullBox, pushBox;

            void Start() {
                audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
                rgbd = GetComponent<Rigidbody>();
                anim = GetComponent<Animator>();
                pushBoxScript = GetComponent<Pushing>();
                ladderClimbScript = GetComponent<Ladder>();
            }

            void Update() {
                //Ladder animation
                if(ladderClimbScript.isClimbing) {
                    LadderClimb();
                    audioController.PlayAudio(AudioType.SFX_ClimbingLadder);
                    return;
                }

                //Lever animation
                if(usingLever) {
                    if(leverScript.leverActive) {
                        anim.Play("CharLeverUp");
                    } else {
                        anim.Play("CharLeverDown");
                    }
                    return;
                }

                //Climbing box animation
                if(climbingBox) {
                    anim.Play("BoxClimb");
                    velocityMagnitude = 0;
                    return;
                }

                //Pushing box animation
                if(pushBoxScript.hasBox) {
                    if(!anim.GetCurrentAnimatorStateInfo(0).IsName("PushToPull")) {
                        anim.Play("PushToPull");
                        Invoke(nameof(CheckPushDirection), 0.05f);
                        return;
                    }

                    CheckPushDirection();
                    return;
                }

                //Walking float change
                if(rgbd.velocity.magnitude > 0.2) {
                    velocityMagnitude += Time.deltaTime * blendSpeed;
                } else {
                    velocityMagnitude -= Time.deltaTime * (blendSpeed * 0.5f);
                }
                velocityMagnitude = Mathf.Clamp(velocityMagnitude, 0, 1);

                //Running float change
                if(Input.GetKey(KeyCode.LeftShift) && rgbd.velocity.magnitude > 0.2) {
                    runFloat += Time.deltaTime * blendSpeed;
                } else {
                    runFloat -= Time.deltaTime * blendSpeed;
                }
                runFloat = Mathf.Clamp(runFloat, 0, 1);

                anim.enabled = true;
                //IdleToWalkToRun animation
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

                anim.SetFloat("LadderBlend", ladderFloat);
                anim.Play("LadderClimb");
            }

            void CheckPushDirection() {
                CancelInvoke();
                switch(pushBoxScript.holdPushDirection) {
                    case Pushing.pushDirection.Left:
                        if(rgbd.velocity.x < 0) {
                            pullBox = true;
                            pushBox = false;
                        } else if(rgbd.velocity.x > 0){
                            pullBox = false;
                            pushBox = true;
                        }
                        break;
                    case Pushing.pushDirection.Down:
                        if(rgbd.velocity.z < 0) {
                            pullBox = true;
                            pushBox = false;
                        } else if(rgbd.velocity.z > 0) {
                            pullBox = false;
                            pushBox = true;
                        }
                        break;
                    case Pushing.pushDirection.Up:
                        if(rgbd.velocity.z > 0) {
                            pullBox = true;
                            pushBox = false;
                        } else if(rgbd.velocity.z < 0) {
                            pullBox = false;
                            pushBox = true;
                        }
                        break;
                    case Pushing.pushDirection.Right:
                        if(rgbd.velocity.x > 0) {
                            pullBox = true;
                            pushBox = false;
                        } else if(rgbd.velocity.x < 0) {
                            pullBox = false;
                            pushBox = true;
                        }
                        break;
                }

                if(rgbd.velocity.magnitude > 0.2f) {
                    anim.enabled = true;
                    audioController.PlayAudio(AudioType.SFX_PullingBoxes);
                } else {
                    anim.enabled = false;
                    audioController.StopAudio(AudioType.SFX_PullingBoxes);
                }

                if(pullBox) {
                    boxPushPullFloat += Time.deltaTime * blendSpeed;
                } else if(pushBox) {
                    boxPushPullFloat -= Time.deltaTime * blendSpeed;
                }
                boxPushPullFloat = Mathf.Clamp(boxPushPullFloat, 0, 1);

                anim.SetFloat("PushToPull", boxPushPullFloat);
                anim.Play("PushToPull");
            }
        }
    }
}
