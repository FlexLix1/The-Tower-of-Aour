using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SlipperyOil : MonoBehaviour
{
    public float speed;
    public bool canMove;
    public Rigidbody rb;

    private PlayerMovement playermovement;

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        rb = gameObject.GetComponent<Rigidbody>();
        playermovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        SlipperyOilMovement();
    }

    public void SlipperyOilMovement()
    {
        if (canMove){ 
            
            if (Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector3(0, 0, 1 * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.velocity = new Vector3(0, 0, -1 * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector3(-1 * speed * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector3(1 * speed * Time.deltaTime, 0, 0);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "OilPuzzleWall")
        {
            canMove = true;
        }
    }
}
