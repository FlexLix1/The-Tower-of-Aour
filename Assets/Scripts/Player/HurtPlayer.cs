using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore {
    namespace Audio {
        public class HurtPlayer : MonoBehaviour {
            public int damageToGive = 1;

            private void OnTriggerEnter(Collider other) {
                if (other.gameObject.tag == "Player") {
                    FindObjectOfType<HealthManager>().HurtPlayer(damageToGive);
                }
            }
        }
    }
}
