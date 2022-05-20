using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird:MonoBehaviour {

    public bool bird;
    bool madeKey;
    public Animator birdAnim;
    Animator thisAnimator;
    GameObject plugInstance, keyInstance;
    public GameObject sparkPlug, keyPrefab, keyPlacement;

    void Start() {
        birdAnim.enabled = false;
        thisAnimator = GetComponent<Animator>();
        thisAnimator.enabled = false;

    }

    void Update() {
        if(!bird)
            return;

        thisAnimator.enabled = true;
        birdAnim.enabled = true;
        if(plugInstance == null)
            plugInstance = Instantiate(sparkPlug, transform.position + transform.up * 2.575f, Quaternion.Euler(-90, 0, 0), transform);


        if(!madeKey) {
            keyInstance = Instantiate(keyPrefab, keyPlacement.transform.position, Quaternion.Euler(0, 90, 0));
            keyInstance.SetActive(false);
            madeKey = true;
            Invoke(nameof(DropKeyFucker), 11);
        }
    }

    void DropKeyFucker() {
        keyInstance.SetActive(true);
    }
}
