using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PressurePlate : MonoBehaviour
{

    public bool OnPressurePlate;
    Transform pressureplate;

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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPressurePlate = false;
            Debug.Log("ohh nnoo MR Left Pressure plate");
            transform.position += new Vector3(0, 0.02f, 0);
        }
    }
}

