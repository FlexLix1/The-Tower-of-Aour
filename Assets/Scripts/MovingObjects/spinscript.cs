using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinscript : MonoBehaviour
{
    public float speedX, speedY, speedZ;
    void Update()
    {
        transform.Rotate(speedX, speedY, speedZ, Space.World);
    }
    
}
