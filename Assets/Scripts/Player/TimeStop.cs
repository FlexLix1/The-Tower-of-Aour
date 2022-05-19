using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityCore {
    namespace Audio {
        public class TimeStop:MonoBehaviour {

            MeshRenderer meshRenderer;
            public Material timeFreezeMat;
            public Material normalMat;

            AudioController audioController;
            GameObject saveFrozenObject;
            Vector3 saveVelocity;

            public GameObject timeFreezeOverlay;
            public bool timeStoped, isRotating;
            public float freezeTime = 5;
            bool usingTimeStop;
            float time;

            public Camera mainCamera;

            void Start() {
                meshRenderer = GetComponent<MeshRenderer>();
            }

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
                        meshRenderer = hit.transform.gameObject.GetComponent<MeshRenderer>();
                        normalMat = meshRenderer.material;
                        saveFrozenObject = hit.transform.gameObject;
                        meshRenderer.material = timeFreezeMat;
                        if(Input.GetMouseButtonDown(0) && !timeStoped) {
                            saveFrozenObject.GetComponent<MovingPlatform>().timeFroze = true;
                            timeStoped = true;
                            usingTimeStop = false;
                        }
                    }
                }
            }

            void Unfreeze() {
                time = 0;
                if(normalMat != null)
                    meshRenderer.material = normalMat;

                normalMat = null;
                meshRenderer = null;
                saveFrozenObject.GetComponent<MovingPlatform>().timeFroze = false;
                saveFrozenObject = null;
                timeStoped = false;
                usingTimeStop = false;
            }
        }
    }
}
