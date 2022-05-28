using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityCore {
    namespace Audio {

        public class HealthManager : MonoBehaviour {
            int maxHealth = 1, damage = 1, currentHealth;
            PlayerMovement playerMovement;
            SkinnedMeshRenderer meshRenderer;
            float respawnTime = 3;

            GameObject player;

            Vector3 respawnPoint;
            bool isRespawning;

            void Start() {
                player = this.gameObject;
                playerMovement = GetComponent<PlayerMovement>();
                meshRenderer = GetComponent<SkinnedMeshRenderer>();
                currentHealth = maxHealth;
                respawnPoint = player.transform.position;
            }

            void Update() {
                //if (Input.GetKeyDown("p")) {
                //    HurtPlayer(damage);
                //}
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