using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityCore {

    namespace Audio {



        public class SlipperyOil : MonoBehaviour {
            public Rigidbody rb;
            Vector3 saveVelocity;
            public AudioController audioController;

            private PlayerMovement playermovement;

            // Start is called before the first frame update
            void Start() {
                rb = gameObject.GetComponent<Rigidbody>();
                playermovement = GetComponent<PlayerMovement>();
            }

            // Update is called once per frame
            void Update() {
                if (playermovement.groundMovement) return;
                rb.velocity = saveVelocity;
            }
            private void OnTriggerEnter(Collider colider) {
                if (colider.gameObject.tag == "OilFloor") {
                    saveVelocity = rb.velocity;
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
                    saveVelocity = rb.velocity;
                    playermovement.groundMovement = false;
                    audioController.StopAudio(AudioType.SFX_GlidingOnOil, true);
                }
                if (colider.gameObject.tag == "OilFloor") {
                    rb.velocity = Vector3.zero;
                    saveVelocity = Vector3.zero;
                    playermovement.groundMovement = true;
                    audioController.StopAudio(AudioType.SFX_GlidingOnOil, true);
                }
            }
        }
    }
}