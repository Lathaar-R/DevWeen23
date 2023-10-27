using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbedState : State
{
    public GrabbedState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void Enter()
    {
        Debug.Log("Entrou no estado de agarrado");
    }

    public override void Exit()
    {
        Debug.Log("Saiu do estado de agarrado");
    }

    public override void Update()
    {
        Debug.Log("Atualizando estado de agarrado");
    }
}
