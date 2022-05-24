using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore {

    namespace Audio {


        public class Elevator:MonoBehaviour {
            [SerializeField] Vector3 elevatorTarget;
            Vector3 elevatorStart;

            public float speed;
            Rigidbody rgbd;
            AudioController audioController;
            public bool elevatorActive;
            bool moveToStart;

            void Start() {
                audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
                rgbd = GetComponent<Rigidbody>();
                elevatorStart = transform.position;
                elevatorTarget += elevatorStart;
            }

            void Update() {
                if(!elevatorActive)
                    return;

                if(moveToStart) {
                    MoveElevatorTo(elevatorStart);
                } else {
                    MoveElevatorTo(elevatorTarget);
                }
            }

            void MoveElevatorTo(Vector3 target) {
                rgbd.constraints = RigidbodyConstraints.FreezeRotation | ~RigidbodyConstraints.FreezePositionY;
                Vector3 forcedDirection = target - transform.position;
                rgbd.velocity = forcedDirection.normalized * speed;
                if(Vector3.Distance(target, transform.position) < 0.1f) {
                    rgbd.constraints = RigidbodyConstraints.FreezeAll;
                    rgbd.velocity = Vector3.zero;
                    moveToStart = !moveToStart;
                    elevatorActive = false;
                }
            }
        }
    }
}
