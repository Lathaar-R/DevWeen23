using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringState : State
{
    public FiringState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void Enter()
    {
        Debug.Log("Entrou no estado de atirar");
    }

    public override void Exit()
    {
        Debug.Log("Saiu do estado de atirar");
    }

    public override void Update()
    {
        Debug.Log("Atualizando estado de atirar");
    }
}
