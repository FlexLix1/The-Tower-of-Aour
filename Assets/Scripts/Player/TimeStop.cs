using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityCore {
    namespace Audio {
        public class TimeStop:MonoBehaviour {

            public Material timeFreezeMat;
            MeshRenderer meshRenderer;
            public Material normalMat;
            Rigidbody rgbd;

            AudioController audioController;
            Vector3 saveVelocity;
            GameObject saveFrozenObject;

            public GameObject timeFreezeOverlay;
            public float freezeTime = 5;
            public bool timeStoped, isRotating;
            bool usingTimeStop;
            float time;

            RaycastHit hit;
            LayerMask mask;
            
            void Start() {
                meshRenderer = GetComponent<MeshRenderer>();
                rgbd = GetComponent<Rigidbody>();
            }

            void Update() {
                if(Input.GetMouseButtonDown(1)) {
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
                    rgbd.velocity = Vector3.zero;
                    return;
                }

                //audioController.StopAudio(AudioType.SFX_TimeStop, true);
                Unfreeze();
            }

            void CheckFreezeItem() {
                timeFreezeOverlay.SetActive(true);
                if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
                    Debug.Log(hit.collider.name);
                    if(hit.transform.gameObject.CompareTag("TimeFreeze")) {
                        if(saveFrozenObject == null || saveFrozenObject != hit.transform.gameObject /*|| meshRenderer.material != timeFreezeMat*/) {
                            meshRenderer = hit.transform.gameObject.GetComponent<MeshRenderer>();
                            normalMat = meshRenderer.material;
                            saveFrozenObject = hit.transform.gameObject;
                        }

                        meshRenderer.material = timeFreezeMat;
                        if(Input.GetMouseButtonDown(0) && !timeStoped) {
                            saveFrozenObject.GetComponent<MovingPlatform>().timeFroze = true;
                            timeStoped = true;
                            usingTimeStop = false;
                        }
                    } else {
                        if(normalMat == null && meshRenderer == null)
                            return;

                        meshRenderer.material = normalMat;
                        normalMat = null;
                        meshRenderer = null;
                        saveFrozenObject = null;
                    }
                }
            }

            void Unfreeze() {
                time = 0;
                if(normalMat != null)
                    meshRenderer.material = normalMat;

                if(isRotating) {
                    rgbd.constraints = ~RigidbodyConstraints.FreezePositionY; // "~" before an constrain sets it to false
                } else {
                    rgbd.constraints = RigidbodyConstraints.None;
                }

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
