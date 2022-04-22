using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WorldBuildingTool:EditorWindow {

    SerializedObject mySerializedWindow;
    SerializedProperty mySerializedGameObjectArray;
    [SerializeField]
    GameObject[] prefabs;
    public GameObject selectedPrefab;
    int selectedPrefabIndex;
    public bool worldToolActive;


    private const string menuItemPath = "Tool/WBT/" + myWindowTitle;
    private const string myWindowTitle = "World Building Tool";

    [MenuItem(menuItemPath)]
    public static void SetupWindow() {
        var window = GetWindow<WorldBuildingTool>(utility: false, title: myWindowTitle + " V_0.0", focus: true);
        window.minSize = new Vector2(400, 175);
        window.maxSize = new Vector2(window.minSize.x + 10, window.minSize.y + 10);
    }

    private void OnEnable() {
        mySerializedWindow = new SerializedObject(this);
        mySerializedGameObjectArray = mySerializedWindow.FindProperty(nameof(prefabs));
    }

    public void OnGUI() {
        EditorGUILayout.PropertyField(mySerializedGameObjectArray);
        string[] options = new string[prefabs.Length];
        for(int i = 0; i < prefabs.Length; i++) {
            options[i] = prefabs[i].name;
        }
        GUILayout.Space(20f);
        
        selectedPrefabIndex = EditorGUILayout.Popup(selectedPrefabIndex, options);
        selectedPrefab = prefabs[selectedPrefabIndex];

        GUILayout.Space(5f);
        if(GUILayout.Button("Create")) {
            GameObject placementIndicator = GameObject.Find("Placement Indicator");
            GameObject holdInstance = Instantiate(selectedPrefab, placementIndicator.transform.position, Quaternion.identity);

            RaycastHit hit;
            if(Physics.Raycast(holdInstance.transform.position, -holdInstance.transform.up, out hit, Mathf.Infinity)) {
                holdInstance.transform.position = new Vector3(hit.point.x, hit.point.y + (holdInstance.transform.localScale.y / 2), hit.point.z);
            }
        }
    }
}

