using UnityEngine;
using UnityEngine.Events;
namespace UnityCore {

    namespace Audio {

        public class PressurePlate : MonoBehaviour {
            public bool pressed;
            float closeDelay = 0.1f;
            public Vector3 pressedOffset = new Vector3(0, 0.02f, 0);
            AudioController audioController;
            Vector3 pressedPosition;
            Transform plateTransform;

            public DynamicDoor doorScript;

            [SerializeField]
            UnityEvent onStandingOnPressurePlate = null;

            [SerializeField]
            UnityEvent onResettingPressurePlate = null;

            void Start() {
                pressed = false;
                plateTransform = transform.GetChild(0).transform;
                pressedPosition = plateTransform.position - pressedOffset;
                audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
            }

            private void OnTriggerEnter(Collider other) {
                if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box")) {
                    CancelInvoke(nameof(Unpressed));
                    //Debug.Log("Hello Mr "+ other.gameObject.name + " who stands on pressure plate", other.gameObject);
                    plateTransform.position = pressedPosition;
                    if (!pressed) {
                        onStandingOnPressurePlate.Invoke();
                    }
                    pressed = true;
                    doorScript.openDoor = true;
                    audioController.PlayAudio(AudioType.SFX_PressurePlate, true);
                }
            }

            private void OnTriggerExit(Collider other) {
                if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box")) {
                    //Use the OverlapBox to detect if there are any other colliders within this box area.
                    //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
                    Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity);

                    foreach (var item in hitColliders) {
                        if (item.gameObject.CompareTag("Player") || item.gameObject.CompareTag("Box")) {
                            return;
                        }
                    }

                    Invoke(nameof(Unpressed), closeDelay);
                }
            }

            private void Unpressed() {
                pressed = false;
                doorScript.openDoor = false;
                plateTransform.position = pressedPosition + pressedOffset * 2;
                onResettingPressurePlate.Invoke();
            }
        }
    }
}

