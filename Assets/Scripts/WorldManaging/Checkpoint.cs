using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore {
    namespace Audio {
        public class Checkpoint : MonoBehaviour {
            public HealthManager theHealthMan;
            public SaveManager theSaveMan;
            public Renderer theRenderer;
            public Material cpOff;
            public Material cpOn;

            void Start() {
                theHealthMan = FindObjectOfType<HealthManager>();
            }
            public void CheckpointOn() {
                Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
                foreach (Checkpoint cp in checkpoints) {
                    cp.CheckpointOff();
                }
                theRenderer.material = cpOn;
            }

            //public void LoadData() {
            //    theSaveMan.LoadPlayer();
            //}

            public void SaveData() {
                theSaveMan.SavePlayer();
            }

            public void CheckpointOff() {
                theRenderer.material = cpOff;
            }
            private void OnTriggerEnter(Collider other) {
                if (other.tag.Equals("Player")) {
                    theHealthMan.SetSpawnPoint(transform.position);
                    CheckpointOn();
                    SaveData();
                }
            }
            private void Update() {
                if (Input.GetKeyDown(KeyCode.I)) {
                    //LoadData();
                }
            }
        }
    }
}
