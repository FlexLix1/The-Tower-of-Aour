using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore {
    namespace Audio {
        public class spinyHurt:MonoBehaviour {

            spinscript spining;
            HurtPlayer hurtPlayer;
            BoxCollider[] colliders;

            void Start() {
                spining = GetComponent<spinscript>();
                hurtPlayer = GetComponent<HurtPlayer>();
                colliders = GetComponents<BoxCollider>();
            }

            void Update() {
                if(spining.timeFroze) {
                    foreach(BoxCollider collider in colliders)
                        collider.isTrigger = false;

                    return;
                }

                foreach(BoxCollider collider in colliders)
                    collider.isTrigger = true;
            }

            void OnTriggerEnter(Collider other) {
                if(!other.CompareTag("Player"))
                    return;

                if(spining.timeFroze)
                    return;

                FindObjectOfType<HealthManager>().HurtPlayer(1);
            }
        }
    }
}
