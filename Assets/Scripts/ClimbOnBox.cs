using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbOnBox : MonoBehaviour
{
   
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Space)){
                Debug.Log("Player found box");
                Player.transform.position += new Vector3(0, 1f, 0);
            }
            
        }
    }
}
