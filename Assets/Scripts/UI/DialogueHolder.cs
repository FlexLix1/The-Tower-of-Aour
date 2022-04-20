using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder:MonoBehaviour {
    [TextArea(4, 10)]
    public string[] dialogueText;
    public string speakerName;
    public bool dialogueDone;
}
