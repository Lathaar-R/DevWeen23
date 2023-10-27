using System;
using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("Atualizando estado de andar");
        //if ta longe o suficiente
        //int x = meu vetor - o do jogador
        // if x > 1
        // dir = abs(x)
        stateMachine.Rb.MovePosition(Vector2.right * Time.deltaTime + stateMachine.Rb.position);
    }
}
