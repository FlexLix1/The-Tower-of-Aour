using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore {

    namespace Audio {


        public class DynamicLever : MonoBehaviour {

            AnimationManager animScript;
            public Elevator elevatorScript;
            public DynamicDoor doorScript;
            public bool leverActive, startTrue, inUse, oneTimeUse, elevator, moveToLever;
            public Animator anim;
            AudioController audioController;

            void Start() {
                audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
                animScript = GameObject.FindGameObjectWithTag("Player").GetComponent<AnimationManager>();
                RaycastHit hit;
                if (Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity)) {
                    transform.position = hit.point;
                }

                if (startTrue) {
                    anim.Play("lever_True");
                    leverActive = true;
                } else {
                    anim.Play("lever_False");
                    leverActive = false;
                }
            }

            public void FlipSwitch() {
                animScript.usingLever = true;
                if (leverActive) {
                    LeverDown();
                } else {
                    LeverUp();
                }
            }

            void LeverUp() {
                inUse = true;
                anim.Play("lever_MTrue");
                audioController.PlayAudio(AudioType.SFX_LeverSound, true);
            }

            void LeverDown() {
                inUse = true;
                anim.Play("lever_MFalse");
                audioController.PlayAudio(AudioType.SFX_LeverSound, true);
            }

            public void SetLeverFalse() {
                anim.Play("lever_False");
                if (elevator) {
                    elevatorScript.elevatorActive = false;
                } else {
                    doorScript.openDoor = false;
                }
                leverActive = false;
                if (oneTimeUse)
                    return;
                inUse = false;
                animScript.usingLever = false;
            }

            public void SetLeverTrue() {
                anim.Play("lever_True");
                if (elevator) {
                    elevatorScript.elevatorActive = true;
                } else {
                    doorScript.openDoor = true;
                }
                leverActive = true;
                if (oneTimeUse)
                    return;
                inUse = false;
                animScript.usingLever = false;
            }
        }
    }
}
