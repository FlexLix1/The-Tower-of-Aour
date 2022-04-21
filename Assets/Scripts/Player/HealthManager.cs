using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {
    public int maxHealth;
    public int currentHealth;

    public GameObject player;

    private bool isRespawning;
    private Vector3 respawnPoint;
    public float respawnTime;

    void Start() {
        currentHealth = maxHealth;

        respawnPoint = player.transform.position;
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
        isRespawning = true;
        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(respawnTime);
        isRespawning = false;

        player.gameObject.SetActive(true);
        player.transform.position = respawnPoint;
        currentHealth = maxHealth;
    }

    public void SetSpawnPoint(Vector3 newPos) {
        respawnPoint = newPos;

    }
}
