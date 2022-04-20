using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{

    public bool OnPressurePlate;
    Transform pressureplate;

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
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPressurePlate = true;
            Debug.Log("Hello Mr who stands on pressure plate");
            transform.position -= new Vector3(0, 0.02f, 0);
            onStandingOnPressurePlate.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Invoke(nameof(PressureplateStartPosition), 5);

        if (waitCoroutineReference != null)
        {
            StopCoroutine(waitCoroutineReference);
        }
        waitCoroutineReference = StartCoroutine(WaitCoroutine(timeUntilReset));

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

