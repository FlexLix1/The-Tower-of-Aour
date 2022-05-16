using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityCore {
    namespace Audio {

        public class Box:MonoBehaviour {

            PlayerMovement movementScript;

            GameObject player;
            public bool hasBox, checkAbove;
            public Vector3 offset;

            Rigidbody rgbd;

            void Start() {
                player = GameObject.FindGameObjectWithTag("Player");
                rgbd = GetComponent<Rigidbody>();
                
            }

            void Update() {
                if(!hasBox) {
                    if(rgbd == null) {
                        rgbd = gameObject.AddComponent<Rigidbody>();
                        rgbd.constraints = RigidbodyConstraints.FreezeRotation | ~RigidbodyConstraints.FreezePositionY;
                    }
                    return;
                }

                Destroy(rgbd);
            }
        }
    }
}
