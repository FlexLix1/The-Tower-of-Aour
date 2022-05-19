using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityCore {
    namespace Audio {

        public class HealthManager : MonoBehaviour {
            public int maxHealth;
            public int currentHealth;
            public PlayerMovement playerMovement;
            public SkinnedMeshRenderer meshRenderer;

            public GameObject player;

            private bool isRespawning;
            private Vector3 respawnPoint;
            public float respawnTime;
            public int damage;

            void Start() {
                currentHealth = maxHealth;
                respawnPoint = player.transform.position;
            }
            private void Update() {
                if (Input.GetKeyDown("p")) {
                    HurtPlayer(damage);
                }
            }
            public void HurtPlayer(int damage) {
                currentHealth -= damage;

                if (currentHealth <= 0) {
                    Respawn();
                }
            }
            public void Respawn() {
                if (!isRespawning) {
                    StartCoroutine("RespawnCount");
                }
            }
            public IEnumerator RespawnCount() {
                playerMovement.enabled = false;
                meshRenderer.enabled = false;
                isRespawning = true;
                yield return new WaitForSeconds(respawnTime);
                playerMovement.enabled = true;
                meshRenderer.enabled = true;
                isRespawning = false;

                player.gameObject.SetActive(true);
                player.transform.position = respawnPoint;
                currentHealth = maxHealth;
            }
            public void SetSpawnPoint(Vector3 newPos) {
                respawnPoint = newPos;
            }
        }
    }
}