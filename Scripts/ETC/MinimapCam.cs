using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCam : MonoBehaviour
{
    public Transform target;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(target.position, transform.position, Time.deltaTime * speed);
        transform.position = new Vector3(transform.position.x, 65f, 20f);
    }
}
