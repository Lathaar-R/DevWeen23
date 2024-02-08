using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbedState : State
{
    private Transform grabberTransform;
    private float multuplier = 8;
    private int direction;
    private float timeToShoot;
    private ParticleSystem.CollisionModule collisionOld;

    public Transform GrabberTransform { get => grabberTransform; set => grabberTransform = value; }

    public GrabbedState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        if (grabberTransform == null)
        {
            grabberTransform = GameObject.FindAnyObjectByType<GrabberScript>().GrabberTransform;
        }
        //stateMachine.Rb.gravityScale = 2;
        stateMachine.Rb.excludeLayers = LayerMask.GetMask("Player");
        stateMachine.gameObject.layer = LayerMask.NameToLayer("GrabbedEnemy");

        //change the cone angle in the gun particle system
        var shape = stateMachine.GunParticleSystem.shape;
        shape.arc = 45;
        shape.rotation = new Vector3(0, 0, -45/2);

        var collision = stateMachine.GunParticleSystem.collision;

        collision.collidesWith = LayerMask.GetMask("Enemy", "Floor", "Floor2", "PassFloor", "Objs");
    }

    public override void Exit()
    {
        

        //change the cone angle in the gun particle system
        var shape = stateMachine.GunParticleSystem.shape;
        shape.arc = 5;
        shape.rotation = new Vector3(0, 0, -2.5f);
    }

    public override void Update()
    {
        timeToShoot -= Time.deltaTime;

        if (grabberTransform != null)
        {
            stateMachine.Rb.velocity = ((Vector2)grabberTransform.position - stateMachine.Rb.position) * multuplier;
            //Incremet the speed in the direction that its going

            //check if the enemy is on the right or left side of the player
            direction = stateMachine.Rb.position.x > stateMachine.PlayerTransform.position.x ? 1 : -1;

            //change the direction of the gun wuth th
            stateMachine.GunTransform.right = direction * Vector2.right;

            //make the gun shoot every 0.5 seconds
            if (timeToShoot < 0)
            {
                GameMenagerScript.Instance.MuzzleFlash(stateMachine.GunParticleSystem.transform.position, stateMachine.GunTransform.right);
                GameMenagerScript.Instance.ShakeCamera(0.1f, 0.1f);
                stateMachine.MuzzleFlashPS.Emit(20);
                GameMenagerScript.Instance.PlayAudio("gun1");
                stateMachine.GunParticleSystem.Emit(1);
                timeToShoot = UnityEngine.Random.Range(0.5f, 1f);
            }
        }
        else
        {
            stateMachine.Rb.velocity *= 2f;

            stateMachine.ChangeState(stateMachine.FallingState);
        }

    }

    public override void FixedUpdate()
    {

    }
}
