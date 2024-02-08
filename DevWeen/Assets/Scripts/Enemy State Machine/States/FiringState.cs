using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringState : State
{
    private float distanceToStop = 0;
    private float timer = 0f;
    private float timeToShoot = 0f;
    public FiringState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void Enter()
    {
        distanceToStop = UnityEngine.Random.Range(8f, 10f);
        timer = 0f;
        timeToShoot = UnityEngine.Random.Range(0.8f, 1f);
    }

    public override void Exit()
    {

        var collision = stateMachine.GunParticleSystem.collision;
        collision.collidesWith = LayerMask.GetMask("Floor", "Floor2", "PassFloor", "Objs", "ObjP", "Player", "GrabbedEnemy");

        stateMachine.Rb.excludeLayers = LayerMask.GetMask("Player", "Enemy", "Floor2");
        stateMachine.gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    public override void Update()
    {
        var pos = (Vector2)stateMachine.PlayerTransform.position - stateMachine.Rb.position;
        //var dir = new Vector2((pos).x, 0);

        if(Vector2.Distance(stateMachine.PlayerTransform.position, stateMachine.Rb.position) > distanceToStop)
        {
            stateMachine.ChangeState(stateMachine.WalkingState);
        }
        else
        {
            timer += Time.deltaTime;

            stateMachine.GunTransform.right = pos.normalized;

            if(timer > timeToShoot)
            {
                stateMachine.GunParticleSystem.Emit(1);
                GameMenagerScript.Instance.MuzzleFlash(stateMachine.GunParticleSystem.transform.position, stateMachine.GunTransform.right);
                GameMenagerScript.Instance.ShakeCamera(0.1f, 0.1f);
                stateMachine.MuzzleFlashPS.Emit(20);
                timer = 0f;
                timeToShoot = UnityEngine.Random.Range(0.6f, 1f);

                GameMenagerScript.Instance.PlayAudio("gun3");
            }   
        }
    }

    public override void FixedUpdate()
    {
        
    }
}
