using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RigidbodyMove : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    private float xRotate = 0.0f;

    public float speed = 10f;
    Rigidbody rb;
    Vector3 dir = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation= Quaternion.identity;
    }

    public float jumpHeight = 3f;

    bool isGrounded;		// ���� ���ִ��� üũ�ϱ� ���� bool��
    public LayerMask ground;	// ���̾��ũ ����
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
        float yRotateSize = Input.GetAxis("Mouse X") + turnSpeed;
        float yRotate = transform.eulerAngles.y + yRotateSize;

        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
    }
}
