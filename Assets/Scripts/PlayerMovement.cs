using UnityEngine;

public class PlayerMovement:MonoBehaviour {

    Vector3 playerPosition = new Vector3();
    public float movementSpeed;
    Rigidbody rgbd;


    void Start() {
        rgbd = GetComponent<Rigidbody>();
    }

    void Update() {
        playerPosition.z = Input.GetAxisRaw("Vertical") * movementSpeed;
        playerPosition.x = Input.GetAxisRaw("Horizontal") * movementSpeed;
        playerPosition.y = rgbd.velocity.y;

        rgbd.velocity = playerPosition;
    }
}
