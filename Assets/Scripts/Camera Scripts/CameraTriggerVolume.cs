using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class CameraTriggerVolume : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private Vector3 boxsize;
    [SerializeField] private Quaternion boxrotation;

    BoxCollider box;
    Rigidbody rb;

    //float? test = null;
    private void Awake()
    {
        box = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        box.isTrigger = true;
        box.size = boxsize;
        box.transform.rotation = boxrotation;
        rb.isKinematic = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxsize);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (CameraSwitcher.ActiveCamera != cam) 
                CameraSwitcher.SwitchCamera(cam);
        }
    }
}

