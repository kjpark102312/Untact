using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;

    public float speed = 3;
    
    private Vector3 moveVector;
    private Rigidbody Myrigidbody;

    private void Start()
    {
        Myrigidbody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        moveVector = new Vector3(h, 0, v).normalized;

        transform.position += moveVector * speed * Time.deltaTime;
    }

}
