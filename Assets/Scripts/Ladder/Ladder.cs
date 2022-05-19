using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityCore {
    namespace Audio {
        public class Ladder:MonoBehaviour {
            public float speed = 10f;
            public bool isClimbing;
            Rigidbody m_Rigidbody;
            PlayerMovement movementScript;
            void Start() {
                m_Rigidbody = GetComponent<Rigidbody>();
                movementScript = GetComponent<PlayerMovement>();
            }

            void Update() {
                if(isClimbing) {
                    movementScript.enabled = false;
                    m_Rigidbody.useGravity = false;
                    m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                    float velocity = Input.GetAxisRaw("Vertical");
                    m_Rigidbody.velocity = new Vector3(0, velocity * speed, 0);

                } else {
                    if(!movementScript.enabled) {
                        movementScript.enabled = true;
                        m_Rigidbody.useGravity = true;
                        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                    }
                }
            }
        }
    }
}
