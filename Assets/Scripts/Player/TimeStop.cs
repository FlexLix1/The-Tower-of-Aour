using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityCore {
    namespace Audio {
        public class TimeStop:MonoBehaviour {

            MeshRenderer meshRenderer;

            AudioController audioController;
            GameObject saveFrozenObject;

            public GameObject timeFreezeOverlay;
            public bool timeStoped, isRotating;
            public float freezeTime = 5;
            bool usingTimeStop;
            float time;

            public Camera mainCamera;

            void Update() {
                if(Input.GetMouseButtonDown(1) && timeStoped) {
                    timeStoped = !timeStoped;
                    Unfreeze();
                }

                if(Input.GetKeyDown(KeyCode.Q) && !timeStoped) {
                    usingTimeStop = !usingTimeStop;
                }

                if(usingTimeStop) {
                    CheckFreezeItem();
                } else {
                    timeFreezeOverlay.SetActive(false);
                }

                if(!timeStoped)
                    return;

                if(time < freezeTime) {
                    time += Time.deltaTime;
                    return;
                }

                //audioController.StopAudio(AudioType.SFX_TimeStop, true);
                Unfreeze();
            }

            void CheckFreezeItem() {
                timeFreezeOverlay.SetActive(true);
                RaycastHit hit;
                if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("TimeFreezeRay"), QueryTriggerInteraction.Collide)) {
                    if(hit.transform.gameObject.CompareTag("Platform")) {
                        saveFrozenObject = hit.transform.gameObject;
                        meshRenderer = saveFrozenObject.GetComponent<MeshRenderer>();
                        meshRenderer.material.EnableKeyword("_EMISSION");

                        if(Input.GetMouseButtonDown(0) && !timeStoped) {
                            saveFrozenObject.GetComponent<MovingPlatform>().timeFroze = true;
                            timeStoped = true;
                            usingTimeStop = false;
                        }
                    }
                } else {
                    if(meshRenderer == null) {
                        meshRenderer.material.DisableKeyword("_EMISSION");
                    }
                }
            }

            void Unfreeze() {
                time = 0;
                if(meshRenderer != null)
                    meshRenderer.material.DisableKeyword("_EMISSION");

                meshRenderer = null;
                saveFrozenObject.GetComponent<MovingPlatform>().timeFroze = false;
                saveFrozenObject = null;
                timeStoped = false;
                usingTimeStop = false;
            }
        }
    }
}
