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

            }

            void Update() {
                if (playermovement.groundMovement) return;
                rb.velocity = saveVelocity;
            }
            private void OnTriggerEnter(Collider colider) {
                if (colider.gameObject.tag == "OilFloor") {
                    onSlipperyOil = true;
                    saveVelocity = rb.velocity.normalized * oilSpeed;
                    playermovement.groundMovement = false;
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
                    saveVelocity = rb.velocity.normalized * oilSpeed;
                    playermovement.groundMovement = false;
                    audioController.StopAudio(AudioType.SFX_GlidingOnOil, true);
                }
                if (colider.gameObject.tag == "OilFloor") {
                    rb.velocity = Vector3.zero;
                    saveVelocity = Vector3.zero;
                    playermovement.groundMovement = true;
                    onSlipperyOil = false;
                    audioController.StopAudio(AudioType.SFX_GlidingOnOil, true);
                }
            }
        }
    }
}