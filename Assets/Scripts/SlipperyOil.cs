using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SlipperyOil : MonoBehaviour
{
    Vector3 saveVelocity;
    public float speed;
    public Rigidbody rb;

    private PlayerMovement playermovement;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        playermovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playermovement.GroundMovement) return;
        rb.velocity = saveVelocity;
    }

    private void OnTriggerEnter(Collider colider)
    {
        if (colider.gameObject.tag == "OilFloor")
        {
            saveVelocity = rb.velocity;
            playermovement.GroundMovement = false;
        }

        if (playermovement.GroundMovement) return;

        if (colider.gameObject.tag == "OilPuzzleWall")
        {
            playermovement.GroundMovement = true;
            saveVelocity = Vector3.zero;
        }
    }
    private void OnTriggerExit(Collider colider)
    {

        if (colider.gameObject.tag == "OilPuzzleWall")
        {
            saveVelocity = rb.velocity;
            playermovement.GroundMovement = false;
        }
        if (colider.gameObject.tag == "OilFloor")
        {
            rb.velocity = Vector3.zero;
            saveVelocity = Vector3.zero;
            playermovement.GroundMovement = true;
        }
    }

    
}
