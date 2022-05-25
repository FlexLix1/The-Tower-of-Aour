using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityCore {
    namespace Audio {
        public class SlipperyOil : MonoBehaviour {

            public bool onSlipperyOil;
            public float oilSpeed;
            Rigidbody rb;
            Vector3 saveVelocity;
            AudioController audioController;

            private PlayerMovement playermovement;

            void Start() {
                audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
                rb = gameObject.GetComponent<Rigidbody>();
                playermovement = GetComponent<PlayerMovement>();

                //rb.velocity = Vector3.ClampMagnitude(rb.velocity, playermovement.movementSpeed);
            }

            void Update() {
                if (playermovement.groundMovement) return;
                rb.velocity = saveVelocity;
            }
            private void OnTriggerEnter(Collider colider) {
                if (colider.gameObject.tag == "OilFloor") {
                    saveVelocity = rb.velocity * oilSpeed;
                    playermovement.groundMovement = false;
                    playermovement.canRun = false;
                    audioController.PlayAudio(AudioType.SFX_GlidingOnOil, true);
                }

                if (colider.gameObject.tag == "OilPuzzleWall") {
                    playermovement.groundMovement = true;
                    saveVelocity = Vector3.zero;
                    audioController.StopAudio(AudioType.SFX_GlidingOnOil, true);
                }
            }

            private void OnTriggerExit(Collider colider) {
                if (colider.gameObject.tag == "OilPuzzleWall") {
                    saveVelocity = rb.velocity * oilSpeed;
                    playermovement.groundMovement = false;
                    audioController.StopAudio(AudioType.SFX_GlidingOnOil, true);
                }
                if (colider.gameObject.tag == "OilFloor") {
                    rb.velocity = Vector3.zero;
                    saveVelocity = Vector3.zero;
                    playermovement.groundMovement = true;
                    playermovement.canRun = true;
                    audioController.StopAudio(AudioType.SFX_GlidingOnOil, true);
                }
            }
        }
    }
}