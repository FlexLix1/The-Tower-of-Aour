using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore {
    namespace Audio {
        public class LadderTrigger:MonoBehaviour {
            Ladder climb;
            GameObject player;
            public BoxCollider upperCollider, lowerCollider;
            bool isTop, isBottom, onLadder;
            AudioController audioController;

            public Cinemachine camera;
            void Start() {
                player = GameObject.FindGameObjectWithTag("Player");
                climb = player.GetComponent<Ladder>();
                audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
            }

            void Update() {
                if(!onLadder)
                    return;

                if(Input.GetKeyDown(KeyCode.E)) {
                    climb.isClimbing = true;
                    player.transform.forward = -transform.forward;
                    Vector3 playerOffset = transform.position + transform.forward;
                    player.transform.position = new Vector3(playerOffset.x, player.transform.position.y, playerOffset.z);
                    audioController.PlayAudio(AudioType.SFX_ClimbingLadder, true);
                }
            }

            void OnTriggerEnter(Collider other) {
                if(!other.CompareTag("Player"))
                    return;

                if(Vector3.Distance(lowerCollider.center, other.transform.position)
                    < Vector3.Distance(upperCollider.center, other.transform.position)) {
                    if(isBottom && !isTop) {
                        climb.isClimbing = false;
                        isBottom = false;
                        audioController.StopAudio(AudioType.SFX_ClimbingLadder, true);
                    } else if(!isBottom && isTop) {
                        climb.isClimbing = false;
                        isTop = false;
                        isBottom = false;
                        audioController.StopAudio(AudioType.SFX_ClimbingLadder, true);
                    } else if(!isBottom && !isTop) {
                        onLadder = true;
                        isTop = false;
                        isBottom = true;
                    }
                }

                if(Vector3.Distance(upperCollider.center, other.transform.position)
                    < Vector3.Distance(lowerCollider.center, other.transform.position)) {
                    if(isTop && !isBottom) {
                        climb.isClimbing = false;
                        isTop = false;
                    } else if(!isTop && isBottom) {
                        climb.isClimbing = false;
                        isTop = false;
                        isBottom = false;
                    } else if(!isTop && !isBottom) {
                        onLadder = true;
                        isTop = true;
                        isBottom = false;
                    }
                }
            }

            void OnTriggerExit(Collider other) {
                if(!other.CompareTag("Player"))
                    return;

                if(!climb.isClimbing) {
                    onLadder = false;
                    isTop = false;
                    isBottom = false;
                }
            }
        }
    }
}