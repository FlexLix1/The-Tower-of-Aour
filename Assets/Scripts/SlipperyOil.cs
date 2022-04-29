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
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "OilPuzzleWall")
        {
            playermovement.GroundMovement = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        canMove = false;
        rb.AddForce(Vector3.forward);
    }
}
