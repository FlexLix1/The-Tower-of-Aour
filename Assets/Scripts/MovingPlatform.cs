using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float distanceToCover;
    [SerializeField] float speed;
    

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Velocity = startPosition;
        Velocity.x += distanceToCover * Mathf.Sin(Time.time * speed);
        transform.position = Velocity;
    }
}
