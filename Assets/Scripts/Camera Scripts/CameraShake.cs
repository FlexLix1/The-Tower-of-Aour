using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private bool shakeIsTrue;
    CinemachineImpulseSource impulse;
    // Start is called before the first frame update
    void Start()
    {
        impulse = transform.GetComponent<CinemachineImpulseSource>();

        shakeIsTrue = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeIsTrue)
        {
            Invoke("shake", 3f);
            shakeIsTrue = false;
        }
    }

    void shake()
    {
        impulse.GenerateImpulse();
        shakeIsTrue = true;
        
    }
}
  