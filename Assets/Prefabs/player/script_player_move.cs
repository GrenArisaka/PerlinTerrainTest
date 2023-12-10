using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_player_move : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float gravity = 9.81f;

    public CharacterController chr;

    Vector3 velocity;

    public Transform groundCh;
    public float srad = 0.4f;
    public LayerMask gMask;
    bool isGrounded;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCh.position, srad, gMask);
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }


        float inputX = Input.GetAxis("Horizontal");
        float InputZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * inputX + transform.forward * InputZ;
        chr.Move(move * moveSpeed * Time.deltaTime);

        velocity.y -= gravity * Time.deltaTime;
        chr.Move(velocity * Time.deltaTime);
    }
}
