using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum EnemyAnimState
    {
        Patroll,
        Chase,
        Attack,
        IsBoom,
        Die
    }

    public enum EnemyType
    {
        Bomber,
        Shooter
    }


    public EnemyAnimState state = EnemyAnimState.Patroll;
    [SerializeField]
    public EnemyType type = EnemyType.Bomber;

    public bool isDie = false;

    [SerializeField]
    private float m_angle = 0f;
    [SerializeField]
    private float m_distance = 0f;
    [SerializeField]
    private LayerMask m_layerMask = 0;
    [SerializeField]
    private float attackDistance = 0f;
    [SerializeField]
    Transform[] wayPoints;
    Transform playerTrm;

    private int destPoint;

    private float attackSpeed = 6f;

    private NavMeshAgent enemyAgent;

    private Transform playerTr;

    public bool bSightInRange;
    public bool bAttackInRange;

    private float remainDistMin = 1f;

    private Animator anim;

    private int attack = Animator.StringToHash("IsAttack");
    private int isBoom = Animator.StringToHash("IsBoom");

    private float attackDelay = 2f;

    public float damage = 3f;

    

    private void Start() 
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        GameObject player = GameObject.FindWithTag("PLAYER");
        if(player != null)
        {
            playerTr = player.transform;
        }
    }

    private void Update() 
    {
        EnemyState();
    }

    void EnemyState()
    {
        bSightInRange = Physics.CheckSphere(transform.position, m_distance, m_layerMask);
        bAttackInRange = Physics.CheckSphere(transform.position, attackDistance, m_layerMask);

        if(!enemyAgent.pathPending&& enemyAgent.remainingDistance <= remainDistMin  &&!bSightInRange && !bAttackInRange) Patrolling();
        if(bSightInRange && !bAttackInRange) ChasePlayer();
        if(bSightInRange && bAttackInRange) Attack();
    }

    void Attack()
    {
        if(type == EnemyType.Bomber && state != EnemyAnimState.IsBoom)
        {
            enemyAgent.SetDestination(playerTr.position);
            enemyAgent.speed = attackSpeed;
            state = EnemyAnimState.Attack;
        }
        else if(type == EnemyType.Shooter)
        {
            enemyAgent.SetDestination(playerTr.position);
            enemyAgent.speed = 0f;
            this.transform.LookAt(playerTr);
            state = EnemyAnimState.Attack;
        }
        PlayAnim();
        Debug.Log("Attack");
        Debug.LogError(state);
    }

    void ChasePlayer()
    {
        enemyAgent.SetDestination(playerTr.position);
        enemyAgent.speed = 3f;
        state = EnemyAnimState.Chase;
        PlayAnim();
        Debug.Log("Chase");
    }

    void Patrolling()
    {
        enemyAgent.speed = 3f;
        if(wayPoints.Length == 0)
        {
            Debug.LogError("최소한 1개 이상의 웨이포인트를 넣으세요");
            enabled = false;
            return;
        }

        enemyAgent.destination = wayPoints[destPoint].position;
        destPoint = (destPoint + 1) % wayPoints.Length;
        state = EnemyAnimState.Patroll;
        PlayAnim();
        Debug.Log("Patrolling");
    }

    void PlayAnim()
    {
        Debug.Log("ASAD");
        switch(state)
        {
            case EnemyAnimState.Patroll:
            anim.SetBool(attack, false);
            break;
            case EnemyAnimState.Chase:
            anim.SetBool(attack, false);
            break;
            case EnemyAnimState.Attack:
            anim.SetBool(attack, true);
            break;
            case EnemyAnimState.IsBoom:
            anim.SetBool(isBoom, true);
            break;
            case EnemyAnimState.Die:
            break;
        }
    }   
    
    private void OnCollisionEnter(Collision other) 
    {
        if(type == EnemyType.Bomber)
        {
            if(other.gameObject.CompareTag("PLAYER"))
            {
                StartCoroutine(Bomber());
            }   
        }
    }
    
    IEnumerator Bomber()
    {
        Debug.LogWarning("BOOM");
        state = EnemyAnimState.IsBoom;
        PlayAnim();
        enemyAgent.speed = 0f;
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);

        //폭발 이펙트
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,  m_distance);

        Gizmos.color = Color.green; 
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }

}
