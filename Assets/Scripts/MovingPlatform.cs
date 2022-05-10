using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector3 platformTarget;

    public bool platFormActive;
    public float speed;

    Vector3 platformStart;

    Rigidbody rgbd;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        platformStart = transform.position;
        platformTarget += platformStart;
        platFormActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (platFormActive)
        {
            if (Vector3.Distance(platformStart, transform.position) > 0.005f)
            {
                MovePlatFormTo(platformStart);
                platFormActive = false;
            }
        }

        if (!platFormActive)
        {
            if (Vector3.Distance(platformTarget, transform.position) > 0.005f)
            {
                MovePlatFormTo(platformTarget);
                platFormActive = true; ;
            }
        }

        void MovePlatFormTo(Vector3 target)
        {
            Vector3 forcedDirection = target - transform.position;
            Vector3.Normalize(forcedDirection);
            rgbd.velocity = forcedDirection * speed;
        }
    }
}
