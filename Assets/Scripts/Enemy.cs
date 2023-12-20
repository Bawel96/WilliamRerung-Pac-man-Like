using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    

    [SerializeField] public List<Transform> wayPoints = new List<Transform>();
    [SerializeField] public float ChaseDistace;
    [SerializeField] public Player Player;

    private BaseState currentState;

    public PatrolState  PatrolState = new PatrolState();
    public ChaseState   ChaseState =  new ChaseState();
    public RetreatState RetreatState = new RetreatState();
    [HideInInspector]public NavMeshAgent navMeshAgent;
    public Animator animator;


    public void SwitchState(BaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentState =  PatrolState;
        currentState.EnterState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
           
    }

    private void Start()
    {
        if(Player != null)
        {
            Player.OnPowerUpStart += StartRetreating;
            Player.OnPowerUpStop += stopRetreating;
        }
    }

    private void Update()
    {
        if(currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    private void StartRetreating()
    {
        SwitchState(RetreatState);
    }

    private void stopRetreating()
    {
        SwitchState(PatrolState);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(currentState != RetreatState)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().dead();
            }

        }
    }
}
