using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {
    public GameObject player;
    public float speed = 10f;
    public bool isClimbing;
    public Rigidbody m_Rigidbody;
    public float exit = 0.1f;
    public float drop = 0.1f;
    //Animator anim;

    void Start() {
        m_Rigidbody = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();
    }

    void Update() {
        if (isClimbing) {
            player.GetComponent<PlayerMovement>().enabled = false;
            m_Rigidbody.useGravity = false;
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            float v = Input.GetAxis("Vertical");
            if (v != 0) {
                player.transform.Translate(Vector3.up * v * speed * Time.deltaTime);
                //anim.SetBool("ClimbUp", true);
                //anim.speed = 1f;
            }
            if (v == -1) {
                player.transform.Translate(Vector3.up * v * speed * Time.deltaTime);
                //anim.SetBool("ClimbDown", true);
                //anim.speed = 1f;
            }
            if (v == 0) {
                //anim.speed = 0f;
            }
            if (Input.GetKey(KeyCode.E)) {
                player.GetComponent<PlayerMovement>().enabled = true;
                m_Rigidbody.useGravity = true;
                m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                //anim.SetBool("ClimbUp", false);
              //  anim.SetBool("ClimbDown", false);
                transform.Translate(Vector3.back * drop);
            }
        } else {
            player.GetComponent<PlayerMovement>().enabled = true;
            m_Rigidbody.useGravity = true;
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            //anim.SetBool("ClimbUp", false);
            //anim.SetBool("ClimbDown", false);

        }
    }

    void OnTriggerExit(Collider Col) {
        if (Col.gameObject.tag == "Ladder") {
            transform.Translate(Vector3.forward * exit);
        }

    }
}
