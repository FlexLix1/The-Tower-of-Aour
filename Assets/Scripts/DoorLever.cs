using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLever:MonoBehaviour {
    
    public void MoveUp() {
        transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
    }

    public void MoveDown() {
        transform.position = new Vector3(transform.position.x, transform.position.y - 5, transform.position.z);
    }
}
