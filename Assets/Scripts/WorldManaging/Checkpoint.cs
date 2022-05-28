using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore {
    namespace Audio {
        public class Checkpoint:MonoBehaviour {
            SaveManager saveScript;

            public void SaveData() {
                saveScript.SavePlayer();
            }

            private void OnTriggerEnter(Collider other) {
                if(!other.CompareTag("Player"))
                    return;

                saveScript = other.GetComponent<SaveManager>();
                saveScript.SavePlayer();
            }
        }
    }
}
