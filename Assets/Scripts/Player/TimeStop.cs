using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityCore {
    namespace Audio {
        public class TimeStop:MonoBehaviour {

            MovingPlatform platformScript;
            spinscript spinscript;

            MeshRenderer meshRenderer;
            AudioController audioController;
            GameObject saveFrozenObject;

            public GameObject timeFreezeOverlay;
            public bool timeStoped;
            public float freezeTime = 5;
            bool usingTimeStop;
            float time;

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

                    if(timeStoped)
                        return;

                    if(platformScript != null) {
                        platformScript.rayIsHovering = false;
                        saveFrozenObject = null;
                        platformScript = null;
                    }

                    if(spinscript != null) {
                        spinscript.rayIsHovering = false;
                        saveFrozenObject = null;
                        spinscript = null;
                    }
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
                        if(saveFrozenObject != hit.transform.gameObject) {
                            if(platformScript != null) {
                                platformScript.rayIsHovering = false;
                                platformScript = null;
                            }
                            saveFrozenObject = hit.transform.gameObject;
                        }
                        platformScript = hit.transform.gameObject.GetComponent<MovingPlatform>();
                        platformScript.rayIsHovering = true;
                        saveFrozenObject = hit.transform.gameObject;

                        if(Input.GetMouseButtonDown(0) && !timeStoped) {
                            platformScript.timeFroze = true;
                            timeStoped = true;
                            usingTimeStop = false;
                        }
                    } else if(hit.transform.gameObject.CompareTag("Spinning")) {
                        if(saveFrozenObject != hit.transform.gameObject) {
                            if(spinscript != null) {
                                spinscript.rayIsHovering = false;
                                spinscript = null;
                            }
                            saveFrozenObject = hit.transform.gameObject;
                        }
                        spinscript = hit.transform.gameObject.GetComponent<spinscript>();
                        spinscript.rayIsHovering = true;
                        saveFrozenObject = hit.transform.gameObject;

                        if(Input.GetMouseButtonDown(0) && !timeStoped) {
                            spinscript.timeFroze = true;
                            timeStoped = true;
                            usingTimeStop = false;
                        }
                    }
                    return;
                }

                if(platformScript != null)
                    platformScript.rayIsHovering = false;

                if(spinscript != null)
                    spinscript.rayIsHovering = false;
            }

            void Unfreeze() {
                time = 0;

                if(platformScript != null) {
                    platformScript.rayIsHovering = false;
                    platformScript.timeFroze = false;
                    saveFrozenObject = null;
                    platformScript = null;
                }

                if(spinscript != null) {
                    spinscript.rayIsHovering = false;
                    spinscript.timeFroze = false;
                    saveFrozenObject = null;
                    spinscript = null;
                }

                timeStoped = false;
                usingTimeStop = false;
            }
        }
    }
}
