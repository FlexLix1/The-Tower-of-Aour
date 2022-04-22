using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{

    public bool OnPressurePlate;
    Transform pressureplate;

    int numOfObjectsOnPlate;

    [SerializeField]
    private float timeUntilReset = 2;

    [SerializeField]
    UnityEvent onStandingOnPressurePlate = null;

    [SerializeField]
    UnityEvent onResettingPressurePlate = null;

    Coroutine waitCoroutineReference;


    // Start is called before the first frame update
    void Start()
    {
        OnPressurePlate = false;
        pressureplate = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(numOfObjectsOnPlate);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (OnPressurePlate) return;

        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            numOfObjectsOnPlate++;
            OnPressurePlate = true;
            Debug.Log("Hello Mr who stands on pressure plate");
            transform.position -= new Vector3(0, 0.02f, 0);
            onStandingOnPressurePlate.Invoke();
            Debug.Log("OnTriggerEnter");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            numOfObjectsOnPlate--;
            if (numOfObjectsOnPlate > 0) return;
            if (waitCoroutineReference != null)
            {
                StopCoroutine(waitCoroutineReference);
            }
            waitCoroutineReference = StartCoroutine(WaitCoroutine(timeUntilReset));
            Debug.Log("OnTriggerExit");
        }
    }
    
    IEnumerator WaitCoroutine(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        PressureplateStartPosition();
    }

    private void PressureplateStartPosition()
    {
        OnPressurePlate = false;
        Debug.Log("You left 5 seconds ago");
        transform.position += new Vector3(0, 0.02f, 0);

        onResettingPressurePlate.Invoke();
    }
}

