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
    }
}
