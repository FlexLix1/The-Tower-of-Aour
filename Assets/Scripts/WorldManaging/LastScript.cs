using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnityCore {
    namespace Audio {
        public class LastScript:MonoBehaviour {

            public RawImage fadeToBlack;
            public GameObject moveTowards;
            GameObject player;
            PlayerMovement movementScript;
            TimeStop timeScript;
            Rigidbody rgbd;

            float fade;
            bool lastBool;

            void Start() {
                player = GameObject.FindGameObjectWithTag("Player");
                rgbd = player.GetComponent<Rigidbody>();
                movementScript = player.GetComponent<PlayerMovement>();
                timeScript = player.GetComponent<TimeStop>();
            }

            void Update() {
                if(!lastBool)
                    return;

                player.transform.forward = -moveTowards.transform.right;
                Vector3 forcedDirection = moveTowards.transform.position - player.transform.position;
                rgbd.velocity = forcedDirection.normalized * 4;
                Debug.Log(fade);
                fadeToBlack.color = new Color(0, 0, 0, fade += Time.deltaTime * 0.25f);
                if(fade >= 1) {
                    SceneManager.LoadScene(3);
                }
            }

            void OnTriggerEnter(Collider other) {
                if(!other.CompareTag("Player"))
                    return;

                movementScript.lastFuckingBool = true;
                timeScript.enabled = false;
                lastBool = true;
                fadeToBlack.enabled = true;
            }
        }
    }
}