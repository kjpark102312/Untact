using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    public float movePower = 1f;
    public float jumpPower = 1f;

    Rigidbody2D rigid;

    Vector3 movement;
    bool isJumping = false;


    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isJumping == false)
            {
                isJumping = true;

                GetComponent<Rigidbody2D>().AddForce(Vector3.up * 300f);
            }

        }
    }
    private void FixedUpdate()
    {
        Move();

    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
        if(collision.gameObject.tag == "Reset")
        {
            //되돌아감
        }
    }
}
