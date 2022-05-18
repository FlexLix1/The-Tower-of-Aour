using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallVisibility:MonoBehaviour {

    public WallVisibility[] makeOtherWallsSolid;
    public GameObject[] walls, disableMiscellaneous;
    public Material original, transparant;

    public bool wallsTransparent;

    MeshRenderer meshRenderer;

    void MakeWallsTransparent() {
        foreach(GameObject wall in walls) {
            wall.GetComponent<MeshRenderer>().material = transparant;
        }

        if(disableMiscellaneous == null)
            return;

        foreach(GameObject miscs in disableMiscellaneous)
            miscs.SetActive(false);
    }

    public void MakeWallsSolid() {
        foreach(GameObject wall in walls) {
            wall.GetComponent<MeshRenderer>().material = original;
        }

        if(disableMiscellaneous == null)
            return;

        foreach(GameObject miscs in disableMiscellaneous)
            miscs.SetActive(true);
    }

    void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player"))
            return;

        if(!wallsTransparent) {
            for(int i = 0; i < makeOtherWallsSolid.Length; i++) {
                makeOtherWallsSolid[i].MakeWallsSolid();
                makeOtherWallsSolid[i].wallsTransparent = false;
            }
            MakeWallsTransparent();
            wallsTransparent = true;
        }
    }
}
