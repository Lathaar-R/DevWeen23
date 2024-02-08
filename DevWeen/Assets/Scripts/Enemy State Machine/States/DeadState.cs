using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    public float timer = 0f;
    private float despawnTimer = 1f;
    public DeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void Enter()
    {
        stateMachine.Rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        stateMachine.Rb.AddTorque(Random.Range(-1f, 1f), ForceMode2D.Impulse);
        
        GameMenagerScript.Instance.PlayAudio("die" + Random.Range(1, 3).ToString());
        GameMenagerScript.Instance.UpdateKills();

        stateMachine.StartCoroutine(stateMachine.Die());
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
        
    }
}
