using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMove : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody rb;
    Vector3 dir = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public float jumpHeight = 3f;

    bool isGrounded;		// 땅에 서있는지 체크하기 위한 bool값
    public LayerMask ground;	// 레이어마스크 설정
    public float groundDistance = 0.2f;

    void Update()
    {
        GroundCheck();
        InputAndDir();
        Jump();
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            Vector3 jumpVelocity = Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            rb.AddForce(jumpVelocity, ForceMode.VelocityChange);
        }
    }

    void GroundCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundDistance, ground))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + dir * speed * Time.deltaTime);
    }

    void InputAndDir()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        if (dir != Vector3.zero)
        {
            transform.forward = dir;
        }
    }
}
