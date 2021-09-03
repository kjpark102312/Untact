using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject Bullet;
    public Transform FirePos;
    private Vector3 dir;
    public Transform Playerpos;
    private bool isTurret;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("SafeZone"))
        {
            isTurret = false;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("TurretZone"))
        {
            isTurret = true;
        }
    }
}
