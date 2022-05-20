using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityCore {
    namespace Audio {


        public class HintManager : MonoBehaviour {
            [SerializeField] GameObject dialogueBox, nextDialogueArrow;
            [SerializeField] Text nameText, dialogueText;
            DialogueManager dialogueManager;
            AudioController audioController;
            HintHolder getHit;

            public float textSpeed = 0.025f;

            string[] holdHint;
            bool hintActive, writingOut;
            int numInDialogue;

            void Start() {
                audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
                dialogueManager = GetComponent<DialogueManager>();
            }

            void Update() {
                if (!hintActive)
                    return;

                if (writingOut && Input.GetKeyDown(KeyCode.E)) {
                    SkipSentence();
                    return;
                }

                if (Input.GetKeyDown(KeyCode.E)) {
                    audioController.PlayAudio(AudioType.SFX_ClickInTextBox);
                    NextSentence();
                }
            }

            void SkipSentence() {
                StopAllCoroutines();
                dialogueText.text = holdHint[numInDialogue];
                nextDialogueArrow.SetActive(false);
                writingOut = false;
                numInDialogue++;
            }

            void NextSentence() {
                if (numInDialogue == getHit.hintText.Length) {
                    nextDialogueArrow.SetActive(false);
                    getHit.hintDone = true;
                    hintActive = false;
                    dialogueBox.SetActive(false);
                    numInDialogue = 0;
                    return;
                }

                StartCoroutine(TypeSentence(holdHint[numInDialogue]));
            }

            void OnTriggerEnter(Collider other) {
                if (other.CompareTag("HintEnd")) {
                    CancelInvoke(nameof(WaitUntilType));
                }

                if (hintActive)
                    return;

                if (other.CompareTag("Hint")) {
                    getHit = other.GetComponent<HintHolder>();
                    if (getHit.hintDone)
                        return;

                    holdHint = new string[getHit.hintText.Length];
                    for (int i = 0; i < holdHint.Length; i++) {
                        holdHint[i] = getHit.hintText[i];
                    }
                    Invoke(nameof(WaitUntilType), getHit.timerSeconds);
                }
            }

            void WaitUntilType() {
                hintActive = true;
                dialogueBox.SetActive(true);
                nameText.text = getHit.speakerName;
                StartCoroutine(TypeSentence(holdHint[numInDialogue]));
            }

            IEnumerator TypeSentence(string sentence) {
                dialogueText.text = "";
                writingOut = true;
                foreach (char letter in sentence.ToCharArray()) {
                    dialogueText.text += letter;
                    yield return new WaitForSeconds(dialogueManager.textSpeed);
                }
                writingOut = false;
                nextDialogueArrow.SetActive(false);
                StopCoroutine(TypeSentence(holdHint[numInDialogue]));
                numInDialogue++;
            }
        }
    }
}
