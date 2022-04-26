using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlacePrefab:Editor {
    WorldBuildingTool tool;

    void OnEnable() {
        //tool =     
    }

    void OnSceneGUI() {
        //if(!tool.worldToolActive)
        //    return;

        if(Input.GetMouseButtonDown(0)) {
            Debug.Log("ndwioad");
        }
    }
}
