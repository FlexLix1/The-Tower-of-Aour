using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallVisibility:MonoBehaviour {

    public WallVisibility[] makeOtherWallsSolid;
    public GameObject[] walls;
    public Material original, transparant;

    public bool wallsTransparent;
    bool transitioning;

    float alphaValue, alphaSpeed = 1f;

    MeshRenderer meshRenderer;

    void Update() {
        if(!transitioning)
            return;

        if(alphaValue <= 0f) {
            transitioning = false;
        }

        alphaValue -= alphaSpeed * Time.deltaTime;
        transparant.color = new Color(1, 1, 1, alphaValue);        
    }

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
            for(int i = 0; i < makeOtherWallsSolid.Length; i++) {
                makeOtherWallsSolid[i].MakeWallsSolid();
                makeOtherWallsSolid[i].wallsTransparent = false;
                makeOtherWallsSolid[i].alphaValue = 1;
                makeOtherWallsSolid[i].transparant.color = Color.white;
            }
            MakeWallsTransparent();
            wallsTransparent = true;
            transitioning = true;
        }
    }
}
