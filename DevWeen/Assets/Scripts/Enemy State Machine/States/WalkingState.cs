using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WalkingState : State
{
    public WalkingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void Enter()
    {
        Debug.Log("Entrou no estado de andar");
    }

    public override void Exit()
    {
        Debug.Log("Saiu do estado de andar");
    }

    public override void Update()
    {
        //Debug.Log("Atualizando estado de andar");
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            float distanceToPlayer = player.transform.position.x - stateMachine.Rb.position.x;
            if(Mathf.Abs(distanceToPlayer) > stateMachine.MinDistanceToPlayer)
            {
                float direction = distanceToPlayer > 0 ? 1f : -1f;
                stateMachine.Rb.MovePosition(Vector2.right * direction * stateMachine.Speed * Time.deltaTime + stateMachine.Rb.position);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.FiringState);
            }
        }
    }
}
