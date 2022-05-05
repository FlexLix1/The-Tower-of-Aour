using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IncreaseOrDecreaseCurrentFloor : MonoBehaviour
{
    public int floorNumber;

    [SerializeField]
    UnityEvent IncreaseCurrentFloor = null;

    [SerializeField]
    UnityEvent DecreaseCurrentFloor = null;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            IncreaseCurrentFloor.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        DecreaseCurrentFloor.Invoke();
    }
}
