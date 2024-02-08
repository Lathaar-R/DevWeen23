using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : State
{
    float timer = 0f;

    public FallingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        timer = 0f;
    }

    public override void Exit()
    {
       
    }

    public override void Update()
    {
       if(stateMachine.Rb.angularVelocity < 0.001f && stateMachine.Rb.angularVelocity > -0.001f && stateMachine.Rb.velocity.magnitude < 0.001f)
       {
            if(timer < 1)
            {
                timer += Time.deltaTime;
            }
            else
            {
                ChangeToWalking();

            }
       }
    }

    private void ChangeToWalking()
    {
        stateMachine.ChangeState(stateMachine.WalkingState);

        var collision = stateMachine.GunParticleSystem.collision;

        collision.collidesWith = LayerMask.GetMask("Floor", "Floor2", "PassFloor", "Objs", "ObjP", "Player", "GrabbedEnemy");

        stateMachine.Rb.excludeLayers = LayerMask.GetMask("Player", "Enemy", "Floor2");
        stateMachine.gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    public override void FixedUpdate()
    {
        
    }
}
