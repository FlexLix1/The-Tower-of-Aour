using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoorWithScript : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoorWithPressurePlate()
    {
        transform.position += new Vector3(0, 5f, 0);
    }

    public void CloseDoorWithPressurePlate()
    {
        transform.position -= new Vector3(0, 5f, 0);
    }
}
