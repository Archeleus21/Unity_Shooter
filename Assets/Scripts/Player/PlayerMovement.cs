using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 6f;  //used for player movement speed

    Vector3 movement;  //stores player transform
    Animator animator;  //stores animator component
    Rigidbody playerRb;  //store rigidbody component

    int floorMask;  //used for layermask, it stores as interger
    float camRayLength = 100f;  //used for raycasting

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");  //gets the layerMask
        animator = GetComponent<Animator>(); //gets the animator component
        playerRb = GetComponent<Rigidbody>(); //gets the rigidbod component
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");  //raw get whole values no decimals
        float vertical = Input.GetAxisRaw("Vertical");

        PlayerMovementControls(horizontal, vertical);
        PlayerTurningControls();
        PlayerAnimation(horizontal, vertical);
    }

    private void PlayerMovementControls(float h, float v)
    {
        movement.Set(h, 0f, v); //used to set movement

        movement = movement.normalized * speed * Time.deltaTime;  //prevents speed increase when moving diagonally

        playerRb.MovePosition(transform.position + movement);  //applies movement to player character
    }

    private void PlayerTurningControls()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);  //stores the position of the mouse relative to the camera

        RaycastHit floorHit;  //used to store the ray variable

        //casting the ray

        if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) //Physics.Raycast(gets stored ray information, pulls floorHit infor, takes distance of raycast, and casts to layer)
        {
            Vector3 playerToMouse = floorHit.point - transform.position;  //used to turn the character where the mouse is
            playerToMouse.y = 0f;  //ensures player doesnt fall over

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);  //used to store rotation from player to mouse

            playerRb.MoveRotation(newRotation);  //applies newRotation
        }
    }

    private void PlayerAnimation(float h, float v)
    {
        bool walking = h != 0 || v != 0;  //checks if walking

        animator.SetBool("isWalking", walking);  //calls animation paramater using walking boolean
    }
}
