using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintHolder:MonoBehaviour {
    [TextArea(4, 10)]
    public string[] hintText;
    public string speakerName = "Hint";
    public bool hintDone;
    public float timerSeconds;
}
