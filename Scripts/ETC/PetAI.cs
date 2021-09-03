using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetAI : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    NavMeshAgent nav;

    Rigidbody rigid;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        FreezeVelocity();
    }
    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(target.position);
    }
    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }
}
