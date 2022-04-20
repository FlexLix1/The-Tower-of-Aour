using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager:MonoBehaviour {
    [SerializeField] GameObject dialogueBox;
    [SerializeField] Text nameText, dialogueText;

    DialogueHolder getDialogue;

    public float textSpeed = 0.025f;

    string[] holdDialogue;
    bool dialogueActive, writingOut;
    int numInDialogue;

    void Update() {
        if(!dialogueActive)
            return;

        if(writingOut && Input.GetKeyDown(KeyCode.E)) {
            StopAllCoroutines();
            dialogueText.text = holdDialogue[numInDialogue];
            writingOut = false;
            numInDialogue++;
            return;
        }

        if(Input.GetKeyDown(KeyCode.E)) {
            NextSentence();
        }
    }

    void NextSentence() {
        if(numInDialogue == getDialogue.dialogueText.Length) {
            getDialogue.dialogueDone = true;
            dialogueActive = false;
            dialogueBox.SetActive(false);
            numInDialogue = 0;
            return;
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(holdDialogue[numInDialogue]));
    }

    void OnTriggerEnter(Collider other) {
        if(dialogueActive)
            return;

        if(other.CompareTag("Dialogue")) {
            getDialogue = other.GetComponent<DialogueHolder>();
            if(getDialogue.dialogueDone)
                return;

            dialogueActive = true;
            dialogueBox.SetActive(true);
            nameText.text = getDialogue.speakerName + ":";
            holdDialogue = new string[getDialogue.dialogueText.Length];
            for(int i = 0; i < holdDialogue.Length; i++) {
                holdDialogue[i] = getDialogue.dialogueText[i];
            }

            StartCoroutine(TypeSentence(holdDialogue[numInDialogue]));
        }
    }

    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        writingOut = true;
        foreach(char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        writingOut = false;
        numInDialogue++;
    }
}
