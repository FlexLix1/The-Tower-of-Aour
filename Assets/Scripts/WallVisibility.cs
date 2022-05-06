using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallVisibility:MonoBehaviour {

    public WallVisibility[] visibility;
    public GameObject[] walls;
    public Material original, transparant;
    public bool wallsTransparent;

    MeshRenderer meshRenderer;

    void MakeWallsTransparent() {
        for(int i = 0; i < walls.Length; i++) {
            walls[i].GetComponent<MeshRenderer>().material = transparant;
        }
    }

    public void MakeWallsSolid() {
        for(int i = 0; i < walls.Length; i++) {
            walls[i].GetComponent<MeshRenderer>().material = original;
        }
    }

    void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player"))
            return;

        if(!wallsTransparent) {
            for(int i = 0; i < visibility.Length; i++) {
                visibility[i].MakeWallsSolid();
                visibility[i].wallsTransparent = false;
            }
            wallsTransparent = true;
            MakeWallsTransparent();
        }
    }
}
