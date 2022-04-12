using UnityEngine;

public class PlayerMovement:MonoBehaviour {

    Vector3 playerPosition = new Vector3();
    public float movementSpeed;
    Rigidbody rgbd;


    void Start() {
        rgbd = GetComponent<Rigidbody>();
    }

    void Update() {
        playerPosition.z = Input.GetAxisRaw("Vertical");
        playerPosition.x = Input.GetAxisRaw("Horizontal");
        playerPosition = playerPosition.normalized * movementSpeed;


        playerPosition.y = rgbd.velocity.y;
        rgbd.velocity = playerPosition;
        Debug.Log(rgbd.velocity);
    }
}
