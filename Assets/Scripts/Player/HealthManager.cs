using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore {
    namespace Audio {
        public class HealthManager : MonoBehaviour {

            int maxHealth = 1, damage = 1, currentHealth;
            float respawnTime = 3;
            bool isRespawning;

            ActiveAndDeactivateFloors currentFloor;
            SkinnedMeshRenderer meshRenderer;
            PlayerMovement playerMovement;

            Vector3 respawnPoint;

            void Start() {
                currentHealth = maxHealth;
                meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
                currentFloor = GetComponent<ActiveAndDeactivateFloors>();
                playerMovement = GetComponent<PlayerMovement>();
            }

            public void HurtPlayer(int damage) {
                currentHealth -= damage;

                if (currentHealth <= 0) {
                    Respawn();
                }
            }

            void Respawn() {
                if (!isRespawning) {
                    StartCoroutine("RespawnCount");
                }
            }

            IEnumerator RespawnCount() {
                isRespawning = true;
                playerMovement.lastFuckingBool = true;
                meshRenderer.enabled = false;
                yield return new WaitForSeconds(respawnTime);
                LoadRespawn();
            }

            void LoadRespawn() {
                Vector3 loadPosition;
                loadPosition.x = PlayerPrefs.GetFloat("PlayerPosX");
                loadPosition.y = PlayerPrefs.GetFloat("PlayerPosY");
                loadPosition.z = PlayerPrefs.GetFloat("PlayerPosZ");
                transform.position = loadPosition;
                meshRenderer.enabled = true;
                currentHealth = maxHealth;

                isRespawning = false;
                playerMovement.lastFuckingBool = false;
            }
        }
    }
}