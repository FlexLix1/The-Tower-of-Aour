using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunConfig_V2:MonoBehaviour {
    Text textField;
    string outputText;
    bool activation, firstLetter, secondLetter, finale;

    void Start() {
        textField = GetComponent<Text>();
    }

    void Update() {
        if(!Input.GetKey(KeyCode.P)) {
            textField.text = null;
            return;
        }

        if(Input.GetKey(KeyCode.U)) {
            outputText = "U";
        }

        if(Input.GetKey(KeyCode.R)) {
            outputText += "R";
        }

        if(Input.GetKey(KeyCode.M)) {
            outputText += " MOM";
        }

        textField.text = outputText;
        outputText = null;
    }
}
